using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqRaporlama
{
    public partial class Form1 : Form
    {
        NorthwindEntities islem = new NorthwindEntities();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void btnFiyataGoreSirala_Click(object sender, EventArgs e)
        {
            var sonuc = from urun in islem.Urunlers
                        orderby urun.BirimFiyati
                        select urun;
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btnUrunAdinaGoreSondan_Click(object sender, EventArgs e)
        {
            var sonuc = from urun in islem.Urunlers
                        orderby urun.UrunAdi descending
                        select urun;
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btnUrunAdinaGoreBastan_Click(object sender, EventArgs e)
        {
            var sonuc = from urun in islem.Urunlers
                        orderby urun.UrunAdi ascending
                        select urun;
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            var sonuc = from urun in islem.Urunlers
                          join tedarikci in islem.Tedarikcilers on urun.TedarikciID equals tedarikci.TedarikciID
                          select new
                          {
                              urun.UrunAdi,
                              urun.BirimFiyati,
                              urun.HedefStokDuzeyi,
                              tedarikci.SirketAdi,
                              tedarikci.Sehir,
                              tedarikci.Telefon
                          };
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var sonuc = from urun in islem.Urunlers
                        join kategori in islem.Kategorilers on urun.KategoriID
                        equals kategori.KategoriID
                        select new
                        {
                            urun.UrunAdi,
                            urun.YeniSatis,
                            urun.HedefStokDuzeyi,
                            kategori.KategoriAdi,
                            kategori.Tanimi
                        };
            dataGridView1.DataSource = sonuc.ToList();
        }

        

        private void btn3_Click(object sender, EventArgs e)
        {
            var sonuc = from kate in islem.Kategorilers
                        join u in islem.Urunlers on kate.KategoriID
                        equals u.KategoriID
                        join t in islem.Tedarikcilers on u.TedarikciID equals t.TedarikciID
                        select new
                        {
                            u.UrunAdi,
                            u.BirimdekiMiktar,
                            kate.KategoriAdi,
                            t.Telefon
                        };
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            var sonuc = from personel in islem.Personellers
                        join satis in islem.Satislars on
                        personel.PersonelID equals satis.PersonelID
                        group satis by personel.Adi into grup
                        select new
                        {
                            personelAdi = grup.Key,
                            toplamSatis = grup.Count()
                        };
            dataGridView1.DataSource = sonuc.ToList();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            var sonuc = from detay in islem.Satis_Detaylaris
                        
                        group detay by detay.Satislar.SatisTarihi.Value.Year into Grup
                        

                        select new
                        {
                            Gelir = Grup.Sum(Satis => Satis.Miktar * Satis.BirimFiyati),
                            Yil = Grup.Key
                        };
            dataGridView1.DataSource = sonuc.ToList();
        }
    }
}
