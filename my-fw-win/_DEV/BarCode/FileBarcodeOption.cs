using System;

namespace ProtocolVN.Framework.Win
{
	public class FileBarcodeOption
	{
        public String idCountry;
        public String idProvider;
        public String idProduct;
        public String stampWidth;//int
        public String stampHeight;//int
        public String bcWidth;//int
        public String bcHeight;//int
        public String bcModule;//float
        public String unitUsing;
        public String unitPos;//int
        public String unitAlight;//int
        public String nameUsing;
        public String namePos;//int
        public String nameAlight;//int
        public String bcUsing;
        public String bcPos;//int
        public String bcAlight;//int
        public String priceUsing;
        public String pricePos;//int
        public String priceAlight;//int

        public String symBC;//int

        private FAConfigOption config;

        public FileBarcodeOption()
        {
            try
            {
                config = new FAConfigOption();
                config.cfgFile = Option.OPTION_FILE;
            }
            catch { }
        }

        public void load()        
        {                      
            this.idCountry = config.GetValue("//option//add[@key='IdCountry']");
            this.idProvider = config.GetValue("//option//add[@key='IdProvider']");
            this.idProduct = config.GetValue("//option//add[@key='IdProduct']");
            this.stampWidth = config.GetValue("//option//add[@key='StampWidth']");
            this.stampHeight = config.GetValue("//option//add[@key='StampHeight']");
            this.bcModule = config.GetValue("//option//add[@key='BCModule']");
            this.bcWidth = config.GetValue("//option//add[@key='BCWidth']");
            this.bcHeight = config.GetValue("//option//add[@key='BCHeight']");
            this.unitUsing = config.GetValue("//option//add[@key='UnitUsing']");
            this.unitPos = config.GetValue("//option//add[@key='UnitPos']");
            this.unitAlight = config.GetValue("//option//add[@key='UnitAlight']");
            this.nameUsing = config.GetValue("//option//add[@key='NameUsing']");
            this.namePos = config.GetValue("//option//add[@key='NamePos']");
            this.nameAlight = config.GetValue("//option//add[@key='NameAlight']");
            this.bcUsing = config.GetValue("//option//add[@key='BCUsing']");
            this.bcPos = config.GetValue("//option//add[@key='BCPos']");
            this.bcAlight = config.GetValue("//option//add[@key='BCAlight']");
            this.priceUsing = config.GetValue("//option//add[@key='PriceUsing']");
            this.pricePos = config.GetValue("//option//add[@key='PricePos']");
            this.priceAlight = config.GetValue("//option//add[@key='PriceAlight']");

            this.symBC = config.GetValue("//option//add[@key='SymBC']");
        }

        public void update()        
        {            
            config.SetValue("//option//add[@key='IdCountry']", idCountry);
            config.SetValue("//option//add[@key='IdProvider']", idProvider);
            config.SetValue("//option//add[@key='IdProduct']", idProduct);
            config.SetValue("//option//add[@key='StampWidth']", stampWidth);
            config.SetValue("//option//add[@key='StampHeight']", stampHeight);
            config.SetValue("//option//add[@key='BCModule']", bcModule);
            config.SetValue("//option//add[@key='BCWidth']", bcWidth);
            config.SetValue("//option//add[@key='BCHeight']", bcHeight);
            config.SetValue("//option//add[@key='UnitUsing']", unitUsing);
            config.SetValue("//option//add[@key='UnitPos']", unitPos);
            config.SetValue("//option//add[@key='UnitAlight']", unitAlight);
            config.SetValue("//option//add[@key='NameUsing']", nameUsing);
            config.SetValue("//option//add[@key='NamePos']", namePos);
            config.SetValue("//option//add[@key='NameAlight']", nameAlight);
            config.SetValue("//option//add[@key='BCUsing']", bcUsing);
            config.SetValue("//option//add[@key='BCPos']", bcPos);
            config.SetValue("//option//add[@key='BCAlight']", bcAlight);
            config.SetValue("//option//add[@key='PriceUsing']", priceUsing);
            config.SetValue("//option//add[@key='PricePos']", pricePos);
            config.SetValue("//option//add[@key='PriceAlight']", priceAlight);

            config.SetValue("//option//add[@key='SymBC']", symBC);
        }        		
	}
}