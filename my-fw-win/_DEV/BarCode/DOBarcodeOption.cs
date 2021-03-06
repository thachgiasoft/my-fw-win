using System;
using ProtocolVN.Framework.Core;
using System.Data;

namespace ProtocolVN.Framework.Win
{
    [Serializable]
	public class DOBarcodeOption : DOData
	{
        public long ID = 1;
        public string COUNTRY = "893";
        public string PROVIDER = "1111";
        public string PRODUCT = "99999";
        public decimal STAMP_WIDTH = 160;
        public decimal STAMP_HEIGHT = 120;
        public decimal BARCODE_WIDTH = 160;
        public decimal BARCODE_HEIGHT = 50;
        public decimal BARCODE_MODULE = 1;
        public string UNIT_USING = "Y";//Y | N : Hiện / Ẩn đơn vị tính
        public int UNIT_POS = 1;
        public int UNIT_ALIGHT = 0;
        public string NAME_USING = "Y";//Y | N : Hiện / Ẩn phần mô tả
        public int NAME_POS = 2;
        public int NAME_ALIGHT = 0;
        public string BARCODE_USING = "Y";//Y | N : Hiện / Ẩn phần mã vạch
        public int BARCODE_POS = 3;
        public int BARCODE_ALIGHT = 0;
        public string PRICE_USING = "Y";//Y | N : Hiện / Ẩn phần số
        public int PRICE_POS = 4;
        public int PRICE_ALIGHT = 0;

        public int SYM_BARCODE = -1;
        public int CHAR_NUMBER = 7;
        public string BARCODE_PARAM = "Cty Phần mềm PROTOCOL";//Tên đơn vị

        static object[] FIELD_MAP = new object[] {             
                "ID",new IDConverter(),
                "COUNTRY",null,
                "PROVIDER",null,
                "STAMP_WIDTH",null,
                "STAMP_HEIGHT",null,
                "BARCODE_WIDTH",null,
                "BARCODE_HEIGHT",null,                      
                "BARCODE_MODULE",null,               
                "UNIT_USING",null,
                "UNIT_POS",null,
                "UNIT_ALIGHT",null,
                "NAME_USING",null,
                "NAME_POS",null,
                "NAME_ALIGHT",null,                      
                "BARCODE_USING",null,               
                "BARCODE_POS",null,               
                "BARCODE_ALIGHT",null,
                "PRICE_USING",null,                      
                "PRICE_POS",null,               
                "PRICE_ALIGHT",null,               
                "SYM_BARCODE",null,
                "CHAR_NUMBER", null,
                "BARCODE_PARAM", null
            };

        public DOBarcodeOption() { }

        public static DOBarcodeOption load()        
        {
            IDataReader reader = null;
            try
            {
                DataSet ds = HelpDB.getDatabase().LoadDataSet("SELECT * FROM FW_BARCODE WHERE ID = 1");
                if (ds == null || ds.Tables.Count == 0)
                {
                    if (DBScriptExec.RunStringSQLScript(
                    @"CREATE TABLE FW_BARCODE (
                        ID              A_BIG_ID NOT NULL /* A_BIG_ID = BIGINT */,
                        COUNTRY         A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                        PROVIDER        A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                        PRODUCT         A_STR_SHORT /* A_STR_SHORT = VARCHAR(100) */,
                        STAMP_WIDTH     A_DOUBLE /* A_DOUBLE = NUMERIC(15,2) default 0 */,
                        STAMP_HEIGHT    A_DOUBLE /* A_DOUBLE = NUMERIC(15,2) default 0 */,
                        BARCODE_WIDTH   A_DOUBLE /* A_DOUBLE = NUMERIC(15,2) default 0 */,
                        BARCODE_HEIGHT  A_DOUBLE /* A_DOUBLE = NUMERIC(15,2) default 0 */,
                        BARCODE_MODULE  A_DOUBLE /* A_DOUBLE = NUMERIC(15,2) default 0 */,
                        UNIT_USING      A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                        UNIT_POS        A_INTEGER /* A_INTEGER = INTEGER */,
                        UNIT_ALIGHT     A_INTEGER /* A_INTEGER = INTEGER */,
                        NAME_USING      A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                        NAME_POS        A_INTEGER /* A_INTEGER = INTEGER */,
                        NAME_ALIGHT     A_INTEGER /* A_INTEGER = INTEGER */,
                        BARCODE_USING   A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                        BARCODE_POS     A_INTEGER /* A_INTEGER = INTEGER */,
                        BARCODE_ALIGHT  A_INTEGER /* A_INTEGER = INTEGER */,
                        PRICE_USING     A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */,
                        PRICE_POS       A_INTEGER /* A_INTEGER = INTEGER */,
                        PRICE_ALIGHT    A_INTEGER /* A_INTEGER = INTEGER */,
                        SYM_BARCODE     A_INTEGER /* A_INTEGER = INTEGER */,
                        CHAR_NUMBER     A_INTEGER /* A_INTEGER = INTEGER */,
                        BARCODE_PARAM   A_STR_MEDIUM /* A_STR_MEDIUM = VARCHAR(200) */
                    );
                    ALTER TABLE FW_BARCODE ADD CONSTRAINT PK_FW_BARCODE PRIMARY KEY (ID);
                    "))
                    {
                        if (DBScriptExec.RunStringSQLScript("INSERT INTO FW_BARCODE (ID, COUNTRY, PROVIDER, PRODUCT, STAMP_WIDTH, STAMP_HEIGHT, BARCODE_WIDTH, BARCODE_HEIGHT, BARCODE_MODULE, UNIT_USING, UNIT_POS, UNIT_ALIGHT, NAME_USING, NAME_POS, NAME_ALIGHT, BARCODE_USING, BARCODE_POS, BARCODE_ALIGHT, PRICE_USING, PRICE_POS, PRICE_ALIGHT, SYM_BARCODE, CHAR_NUMBER, BARCODE_PARAM) VALUES (1, '893', '111', '123456', 200, 120, 200, 50, 1, 'Y', 1, 0, 'Y', 2, 0, 'Y', 3, 0, 'Y', 4, 0, -1, 5, '');") == false)
                        {
                            return new DOBarcodeOption();
                        }
                    }
                }
                reader = DatabaseFB.LoadRecord("FW_BARCODE", "ID", 1);
                using (reader)
                {
                    if (reader.Read())
                    {
                        DataTypeBuilder dt = new DataTypeBuilder(FIELD_MAP);
                        return (DOBarcodeOption)dt.CreateFilledObjectExt(typeof(DOBarcodeOption), reader);
                    }
                }
            }
            catch { }
            finally {
                if (reader != null) reader.Close();
            }
            return new DOBarcodeOption();
        }

        public void update()        
        {
            DataSet ds = new DataSet();
            HelpDB.getDatabase().LoadDataSet(ds, "SELECT * FROM FW_BARCODE WHERE ID = 1", "FW_BARCODE");            
            DataRow row = ds.Tables[0].Rows[0];

            row["ID"] = ID;
            row["COUNTRY"] = COUNTRY;
            row["PROVIDER"] = PROVIDER;
            row["STAMP_WIDTH"] = STAMP_WIDTH;
            row["STAMP_HEIGHT"] = STAMP_HEIGHT;
            row["BARCODE_WIDTH"] = BARCODE_WIDTH;
            row["BARCODE_HEIGHT"] = BARCODE_HEIGHT;                      
            row["BARCODE_MODULE"] = BARCODE_MODULE;
            row["UNIT_USING"] = UNIT_USING;
            row["UNIT_POS"] = UNIT_POS;
            row["UNIT_ALIGHT"] = UNIT_ALIGHT;
            row["NAME_USING"] = NAME_USING;
            row["NAME_POS"] = NAME_POS;
            row["NAME_ALIGHT"] = NAME_ALIGHT;
            row["BARCODE_USING"] = BARCODE_USING;
            row["BARCODE_POS"] = BARCODE_POS;
            row["BARCODE_ALIGHT"] = BARCODE_ALIGHT;
            row["PRICE_USING"] = PRICE_USING;
            row["PRICE_POS"] = PRICE_POS;         
            row["PRICE_ALIGHT"] = PRICE_ALIGHT;
            row["SYM_BARCODE"] = SYM_BARCODE;
            row["CHAR_NUMBER"] = CHAR_NUMBER;
            row["BARCODE_PARAM"] = BARCODE_PARAM;
            HelpDB.getDatabase().UpdateDataSet(ds);            
        }        		
	}
}