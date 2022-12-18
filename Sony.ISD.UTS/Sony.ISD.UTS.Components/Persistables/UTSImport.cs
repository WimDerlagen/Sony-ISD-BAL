using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class UTSImport
    {
        private string vestiging;
        private int vestigingId;
        private string hoofdcategorie;
        private string subcategorie;
        private int artikelId;
        private int artikelnummer;
        private string omschrijving;
        private string type;
        private string merk;
        private string kleur;
        private string afmeting;
        private decimal volume;
        private string opmerkingen;
        private string afbeelding;
        private int aantal;
        private int totaal;

        public string Vestiging
        {
            get { return vestiging; }
            set { vestiging = value; }
        }
        public int VestigingID
        {
            get { return vestigingId; }
            set { vestigingId = value; }
        }
        public string Hoofdcategorie
        {
            get { return hoofdcategorie; }
            set { hoofdcategorie = value; }
        }
        public string Subcategorie
        {
            get { return subcategorie; }
            set { subcategorie = value; }
        }
        public int ArtikelID
        {
            get { return artikelId; }
            set { artikelId = value; }
        }
        public int ArtikelNummer
        {
            get { return artikelnummer; }
            set { artikelnummer = value; }
        }
        public string Omschrijving
        {
            get { return omschrijving; }
            set { omschrijving = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Merk
        {
            get { return merk; }
            set { merk = value; }
        }
        public string Kleur
        {
            get { return kleur; }
            set { kleur = value; }
        }
        public string Afmeting
        {
            get { return afmeting; }
            set { afmeting = value; }
        }
        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        public string Opmerkingen
        {
            get { return opmerkingen; }
            set { opmerkingen = value; }
        }
        public string Afbeelding
        {
            get { return afbeelding; }
            set { afbeelding = value; }
        }
        public int Aantal
        {
            get { return aantal; }
            set { aantal = value; }
        }
        public int Totaal
        {
            get { return totaal; }
            set { totaal = value; }
        }
    }
}
