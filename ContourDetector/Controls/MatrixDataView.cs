using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetector.Controls
{
    class MatrixDataView<T>
    {
        private int _defaultSize = 3;

        private T[,] _sourceArray = null;

        public MatrixDataView(T[,] sourceArray)
        {
            this._sourceArray = sourceArray;
        }

        public MatrixDataView()
        {
            
        }

        public DataView BindArrayToDataView<T>()
        {
            DataView dataView;
            if (this._sourceArray == null)
            {
                dataView = this.FormatDataViewWithoutSource();
            }
            else
            {
                dataView = this.FormatDataViewWithSource(this._sourceArray);
            }

            return dataView;            
        }

        private DataView FormatDataViewWithSource(T[,] source)
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < source.GetLength(1); i++)
            {
                dataTable.Columns.Add(i.ToString(), typeof(Ref<T>));
            }
            for (int i = 0; i < source.GetLength(0); i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataTable.Rows.Add(dataRow);
            }
            DataView dataView = new DataView(dataTable);
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    int a = i;
                    int b = j;
                    Ref<T> refT = new Ref<T>(() => source[a, b], z => { source[a, b] = z; });
                    dataView[i][j] = refT;
                }
            }
            return dataView;            
        }

        private DataView FormatDataViewWithoutSource()
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < this._defaultSize; i++)
            {
                dataTable.Columns.Add(i.ToString(), typeof(Ref<T>));
            }
            for (int i = 0; i < this._defaultSize; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataTable.Rows.Add(dataRow);
            }
            DataView dataView = new DataView(dataTable);
            return dataView;
        }
    }
}
