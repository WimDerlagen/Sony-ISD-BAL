using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class Product
    {
        private int productId;
        private string productIdentificatie;
        private string productNaam;
        private string serieNummer;
        private int productGroepId;
        private DateTime datumEersteInbreng;
        private ProductStatus productStatus;
        private string uitwendigeStaat;
        private bool sample;
        private decimal fvalue;
        private int magazijnLocatie;
        private int quantity;

        private ProductGroup productGroup;

        public int ProductID
        {
            get { return productId; }
            set { productId = value; }
        }
        public string ProductIdentificatie
        {
            get { return productIdentificatie; }
            set { productIdentificatie = value; }
        }
        public string ProductNaam
        {
            get { return productNaam; }
            set { productNaam = value; }
        }
        public string SerieNummer
        {
            get { return serieNummer; }
            set { serieNummer = value; }
        }
        public int ProductGroepID
        {
            get { return productGroepId; }
            set { productGroepId = value; }
        }
        public DateTime DatumEersteInbreng
        {
            get { return datumEersteInbreng; }
            set { datumEersteInbreng = value; }
        }
        public ProductStatus ProductStatus
        {
            get { return productStatus; }
            set { productStatus = value; }
        }
        public string UitwendigeStaat
        {
            get { return uitwendigeStaat; }
            set { uitwendigeStaat = value; }
        }
        public bool Sample
        {
            get { return sample; }
            set { sample = value; }
        }
        public decimal FiscalValue
        {
            get { return fvalue; }
            set { fvalue = value; }
        }
        public int MagazijnLocatie
        {
            get { return magazijnLocatie; }
            set { magazijnLocatie = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public ProductGroup ProductGroup
        {
            get { return productGroup; }
            set { productGroup = value; }
        }

        public string GetUrl()
        {
            throw new Exception("to implement");
        }
    }
}
