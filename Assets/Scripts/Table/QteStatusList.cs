//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XTable {
    
    
    public class QteStatusList : CVSReader {
        
        public class RowData{
			public string Comment;
			public string Name;
			public int Value;
		}


		private static QteStatusList s = null;

		public static QteStatusList sington
		{
			get { if (s == null) { s = new QteStatusList(); s.Create(); } return s; }
		}

		public RowData[] Table { get { return table; } }

		private RowData[] table = null;

		public override string bytePath { get { return "Table/QteStatusList"; } }
        
        public override void OnClear(int lineCount) {
			if (lineCount > 0) table = new RowData[lineCount];
			else table = null;
        }
        
        public override void ReadLine(System.IO.BinaryReader reader) {
			RowData row = new RowData();
			Read<string>(reader, ref row.Comment, stringParse); columnno = 0;
			Read<string>(reader, ref row.Name, stringParse); columnno = 1;
			Read<int>(reader, ref row.Value, intParse); columnno = 2;
			Table[lineno] = row;
			columnno = -1;
        }
    }
}
