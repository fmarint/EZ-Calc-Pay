namespace EZCalcPay
{
   public partial class MainPage : ContentPage
   {

      public MainPage()
      {
         InitializeComponent();
         btnMinus.IsEnabled = false;
      }

      decimal billBase = 0;
      int numPersons = 1;
      int tipPercent = 0;

      private void txtBill_Completed(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(txtBill.Text))
            txtBill.Text = "0.00";

         billBase = decimal.Parse(txtBill.Text);
         lblTotal.Text = $"{billBase:C}";
         CalcTotalByPerson();
      }

      private void CalcTotalByPerson()
      {
         var totalTip = (billBase * tipPercent) / 100;

         var tipByPerson = (totalTip / numPersons);
         lblTip.Text = $"{tipByPerson:C}";

         var subTotal = billBase / numPersons;
         lblSubTotal.Text = $"{subTotal:C}";

         var totalByPerson = (tipByPerson + subTotal);
         lblTotal.Text = $"{totalByPerson:C}";
      }

      private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
      {
         tipPercent = (int)e.NewValue;
         lblTipSlider.Text = $"Tip {tipPercent.ToString()}%:";
         CalcTotalByPerson();
         SetShadowButtonTip(null);


      }
      private void Button_Clicked(object sender, EventArgs e)
      {
         if (sender is Button btn)
         {
            tipPercent = int.Parse(btn.Text.Replace("%", ""));
            sliderTip.Value = tipPercent;
            CalcTotalByPerson();
            SetShadowButtonTip(btn);
         }
      }

      private void SetShadowButtonTip(Button? btn)
      {
         if (btn == null)
         {
            if (tipPercent == 10)
               btn = btnTen;
            else if (tipPercent == 15)
               btn = btnFifteen;
            else if (tipPercent == 20)
               btn = btnTwenty;
         }

         btnTen.Shadow ??= new Shadow();
         btnFifteen.Shadow ??= new Shadow();
         btnTwenty.Shadow ??= new Shadow();

         btnTen.Shadow.Brush = null;
         btnFifteen.Shadow.Brush = null;
         btnTwenty.Shadow.Brush = null;

         if (btn == btnTen)
         {
            btnTen.Shadow.Radius = 12;
            btnTen.Shadow.Brush = new SolidColorBrush(Colors.White);
         }
         else if (btn == btnFifteen)
         {
            btnFifteen.Shadow.Radius = 12;
            btnFifteen.Shadow.Brush = new SolidColorBrush(Colors.White);
         }
         else if (btn == btnTwenty)
         {
            btnTwenty.Shadow.Radius = 12;
            btnTwenty.Shadow.Brush = new SolidColorBrush(Colors.White);
         }
      }

      private void btnMinus_Clicked(object sender, EventArgs e)
      {
         if (numPersons > 1)
         {
            numPersons--;
            lblPersons.Text = numPersons.ToString();
            CalcTotalByPerson();
         }
         if (numPersons == 1)
            btnMinus.IsEnabled = false;
      }

      private void btnPlus_Clicked(object sender, EventArgs e)
      {
         numPersons++;
         lblPersons.Text = numPersons.ToString();
         CalcTotalByPerson();
         if (numPersons > 1)
            btnMinus.IsEnabled = true;
      }

   }
}
