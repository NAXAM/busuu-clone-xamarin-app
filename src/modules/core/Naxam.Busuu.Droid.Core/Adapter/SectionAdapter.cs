using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Widget.AdapterView;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.Views;

namespace Naxam.Busuu.Droid.Core.Adapter
{
    public abstract class SectionAdapter : BaseAdapter, IOnItemClickListener  
    {
        private int mCount = -1;

        public abstract int NumberOfSections();

        public abstract int NumberOfRows(int section);

        public abstract View GetRowView(int section, int row, View convertView, ViewGroup parent);

        public abstract Java.Lang.Object GetRowItem(int section, int row);

        public virtual bool HasSectionHeaderView(int section)
        {
            return false;
        }

        public virtual View GetSectionHeaderView(int section, View convertView, ViewGroup parent)
        {
            return null;
        }

        public virtual Java.Lang.Object GetSectionHeaderItem(int section)
        {
            return null;
        }

        public virtual int GetRowViewTypeCount()
        {
            return 1;
        }

        public virtual int GetSectionHeaderViewTypeCount()
        {
            return 1;
        }
        public virtual void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            OnRowItemClick(parent, view, GetSection(position), GetRowInSection(position), id);
        }
        public virtual void OnRowItemClick(AdapterView parent, View view, int section, int row, long id)
        {

        }

        public  override sealed int Count
        {
            get
            {
                if (mCount < 0)
                {
                    mCount = NumberOfCellsBeforeSection(NumberOfSections());
                }
                return mCount;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }


        public override sealed Java.Lang.Object GetItem(int position)
        {
            int section = GetSection(position);
            if (IsSectionHeader(position))
            {
                if (HasSectionHeaderView(section))
                {
                    return GetSectionHeaderItem(section);
                }
                return null;
            }
            return GetRowItem(section, GetRowInSection(position));
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override sealed View GetView(int position, View convertView, ViewGroup parent)
        {
            int section = GetSection(position);
            if (IsSectionHeader(position))
            {
                if (HasSectionHeaderView(section))
                {
                    return GetSectionHeaderView(section, convertView, parent);
                }
                return null;
            }
            return GetRowView(section, GetRowInSection(position), convertView, parent);
        }

        /**
         * Returns the section number of the indicated cell
         */
        public virtual int GetSection(int position)
        {
            int section = 0;
            int cellCounter = 0;
            while (cellCounter <= position && section <= NumberOfSections())
            {
                cellCounter += numberOfCellsInSection(section);
                section++;
            }
            return section - 1;
        }

        /**
         * Returns the row index of the indicated cell Should not be call with
         * positions directing to section headers
         */
        public virtual int GetRowInSection(int position)
        {
            int section = GetSection(position);
            int row = position - NumberOfCellsBeforeSection(section);
            if (HasSectionHeaderView(section))
            {
                return row - 1;
            }
            else
            {
                return row;
            }
        }

        /**
         * Returns true if the cell at this index is a section header
         */
        public virtual bool IsSectionHeader(int position)
        {
            int section = GetSection(position);
            return HasSectionHeaderView(section) && NumberOfCellsBeforeSection(section) == position;
        }

      
        protected virtual int NumberOfCellsBeforeSection(int section)
        {
            int count = 0;
            for (int i = 0; i < Math.Min(NumberOfSections(), section); i++)
            {
                count += numberOfCellsInSection(i);
            }
            return count;
        }

        private int numberOfCellsInSection(int section)
        {
            return NumberOfRows(section) + (HasSectionHeaderView(section) ? 1 : 0);
        }

     
    public override void NotifyDataSetChanged()
        {
            mCount = NumberOfCellsBeforeSection(NumberOfSections());
            base.NotifyDataSetChanged();
        }

     
    public override void NotifyDataSetInvalidated()
        {
            mCount = NumberOfCellsBeforeSection(NumberOfSections());
            base.NotifyDataSetInvalidated();
        }

        public int GetRowItemViewType(int section, int row)
        {
            return 0;
        }

    
        public virtual int GetSectionHeaderItemViewType(int section)
        {
            return 0;
        }

        public override sealed int GetItemViewType(int position)
        {
            int section = GetSection(position);
            if (IsSectionHeader(position))
            {
                return GetRowViewTypeCount() + GetSectionHeaderItemViewType(section);
            }
            else
            {
                return GetRowItemViewType(section, GetRowInSection(position));
            }
        }

        public override sealed int ViewTypeCount
        {
            get
            {
                return GetRowViewTypeCount() + GetSectionHeaderViewTypeCount();
            }
        }
         
        public override bool IsEnabled(int position)
        {
            return (DisableHeaders() || !IsSectionHeader(position)) && IsRowEnabled(GetSection(position), GetRowInSection(position));
        }

        public  bool DisableHeaders()
        {
            return false;
        }

        public bool IsRowEnabled(int section, int row)
        {
            return true;
        }
    }
}