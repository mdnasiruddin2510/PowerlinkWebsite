namespace PosWebsite.Helper;
public class SMSBody
{
    public string SalesSummaryToCustomer = "মোট বিলঃ {0} টাকা। পরিশোধঃ {1} টাকা, বকেয়াঃ {2} টাকা। মোট বকেয়াঃ {3} টাকা\n{4}";
    public string CustomerPaymentToCustomer = "আপনার পরিশোধিত {0} টাকা  {1} বাবদ সফলভাবে গ্রহন করা হয়েছে। অবশিষ্ট বকেয়াঃ {2} টাকা\n{3}";
    public string CustomerAdvancePaymentToCustomer = "{0} টাকা অগ্রিম পেমেন্ট হিসাবে গ্রহন করা হয়েছে।";
    public string AdjustCustomerPaymentToCustomer = "প্রিয়,{0} টাকা  {1}লেজার ব্যালেন্স অ্যাডজাস্ট করা হয়েছে। আপনা বর্তমান ব্যালেন্সঃ{2}\n{3}";
    public string AdjustCustomerAdvancePaymentToCustomer = "প্রিয়,{0} টাকা আপনার অগ্রিম পেমেন্ট থেকে বিক্রি  বকেয়া অ্যাডজাস্ট করা হয়েছে।\n{1}";
    public string CustomerBalanceAcknowledgement = "প্রিয়, আপনার বর্তমান ব্যালেন্স {0}টাকা। আপনাকে স্বল্প সময়ের মধ্যে বকেয়া পরিশোধ করার জন্য অনুরোধ করা হচ্ছে।\n{1}";

    public string CustomerOrderGreetingsToCustomer = "আপনার অর্ডার সফলভাবে সম্পন্ন হয়েছে। আমরা শীঘ্রই যোগাযোগ করব। মোট  বিলঃ {0} টাকা। পরিশোধঃ {1} টাকা, বকেয়াঃ {2} টাকা\n{3}";



    public string PurchaseSummaryToSupplier = "মোট  বিলঃ {0} টাকা। পরিশোধঃ {1} টাকা, বকেয়াঃ {2} টাকা। মোট বকেয়াঃ {3} টাকা\n{4}";
    public string SupplierPaymentToSupplier = "পেমেন্ট {0} টাকা সফলভাবে পরিশোধ করা হয়েছে। {1}। অবশিষ্ট বকেয়াঃ {2} টাকা\n{3}";
    public string SupplierAdvancePaymentToSupplier = "{0} টাকা অগ্রিম পেমেন্ট হিসাবে  সফলভাবে পরিশোধ করা হয়েছে।";
    public string AdjustSuuplierAdvancePaymentToSupplier = "প্রিয়,{0} টাকা আপনার অগ্রিম পেমেন্ট থেকে বিক্রি  বকেয়া অ্যাডজাস্ট করা হয়েছে।\n{1}";




    public string SalesSummaryToOwner = "{0} বিক্রির মোট  বিলঃ {1} টাকা। পরিশোধঃ{2} টাকা, বকেয়াঃ{3} টাকা। মোট বকেয়াঃ {4} টাকা।";
    public string PurchaseSummaryToOwner = "{0} ক্রয়ের মোট  বিলঃ {1} টাকা। পরিশোধঃ{2} টাকা, বকেয়াঃ{3} টাকা। মোট বকেয়াঃ {4} টাকা।";
    public string CustomerPaymentToOwner = "{0} টাকা  {1} এর মাধ্যমে গ্রহন করা হয়েছে। {2}  অবশিষ্ট বকেয়াঃ {3} টাকা।";
    public string CustomerAdvancePaymentToOwner = "{0} টাকা  {1} এর থেকে অগ্রিম পেমেন্ট হিসাবে গ্রহন করা হয়েছে।";
    public string SupplierPaymentToOwner = "{0} টাকা  {1} কে {2} এর মাধ্যমে পরিশোধ করা হয়েছে। অবশিষ্ট বকেয়াঃ {3} টাকা।";
    public string SupplierAdvancePaymentToOwner = "{0} টাকা  {1} কে অগ্রিম বাবদ প্রদান করা হয়েছে।";
    public string ExpenseSmsToOwner = "{0} টাকা ব্যয় হয়েছে";
    public string IncomeSmsToOwner = "{0} টাকা আয় হিসাবে গ্রহন করা হয়েছে।";
    public string AdjustCustomerPaymentToOwner = "{0} টাকা  {1} এর থেকে  {2} লেজার ব্যালেন্স হিসাবে অ্যাডজাস্ট করা হয়েছে। বর্তমান ব্যালেন্সঃ{3}\n";
    public string AdjustCustomerAdvancePaymentToOwner = "{0} টাকা {1} এর  ক্রয়ের বকেয়ার সাথে অগ্রিম পেমেন্ট অ্যাডজাস্ট করা হয়েছে। ";
    public string AdjustSupplierAdvancePaymentToOwner = "{0} টাকা {1} এর  ক্রয়ের বকেয়ার সাথে অগ্রিম পেমেন্ট অ্যাডজাস্ট করা হয়েছে। ";
    public string LoanPaymentToOwner = "{0} টাকা  {1} কে লোন পেমেন্ট হিসাবে প্রদান করা হয়েছে।";
    public string LoanReceivedToOwner = "{0} টাকা  {1} এর থেকে লোন পরিশোধ হিসাবে গ্রহন করা হয়েছে।";
    public string transferAmountToOwner = "{0} টাকা  {1} থেকে {2} তে সফলভাবে ট্রান্সফার করা হয়েছে।";
    public string investmentAmountToOwner = "{0} টাকা এই {1} খাতে বিনিয়োগ করা হয়েছে।";
    public string withdrawAmountToOwner = "{0} টাকা  {1} থেকে সফলভাবে উত্তোলন করা হয়েছে।";

    public string sendOTP = "আপনার এককালীন পিন {0}\n{1}";





    //public string SalesSummaryToCustomer = "Total bill: {0}Tk. Paid: {1}Tk, Due: {2}Tk. Total Due: {3}Tk.\nFrom\n{4}";
    //public string CustomerPaymentToCustomer = "Your payment {0}Tk has been received successfully for {1}. Remaining Due: {2}Tk.\nFrom\n{3}";
    //public string CustomerAdvancePaymentToCustomer = "{0}Tk received as advance payment successfully!";
    //public string AdjustCustomerPaymentToCustomer = "Dear,{0}Tk has been {1} as ledger balance adjustment.Your current balance:{2}.\nFrom\n{3}";
    //public string AdjustCustomerAdvancePaymentToCustomer = "Dear,{0}Tk has been adjusted with sales due from your advance payment.\nFrom\n{1}";
    //public string CustomerBalanceAcknowledgement = "Dear,Your current balance is {0}Tk. You are requested to pay the due in short time.\nFrom\n{1}";

    //public string CustomerOrderGreetingsToCustomer = "Your order has been placed successfully! We will contact with you soon. Your total bill: {0}Tk. Paid: {1}Tk, Due: {2}Tk.\nFrom\n{3}";



    //public string PurchaseSummaryToSupplier = "Total bill: {0}Tk. Paid: {1}Tk, Due: {2}Tk. Total Due: {3}Tk.\nFrom\n{4}";
    //public string SupplierPaymentToSupplier = "The payment {0}Tk has been paid successfully for {1}. Remaining Due: {2}Tk.\nFrom\n{3}";
    //public string SupplierAdvancePaymentToSupplier = "{0}Tk paid as advance payment successfully!";
    //public string AdjustSuuplierAdvancePaymentToSupplier = "Dear,{0}Tk has been adjusted with sales due from your advance payment.\nFrom\n{1}";




    //public string SalesSummaryToOwner = "{0} Total bill of sales: {1}Tk. Paid:{2}Tk, Due:{3}Tk. Total Due: {4}Tk.";
    //public string PurchaseSummaryToOwner = "{0} Total bill of purchase: {1}Tk. Paid:{2}Tk, Due:{3}Tk. Total Due: {4}Tk.";
    //public string CustomerPaymentToOwner = "{0}Tk received from {1} by {2}. Remaining Due: {3}Tk.";
    //public string CustomerAdvancePaymentToOwner = "{0}Tk received from {1} as advance payment.";
    //public string SupplierPaymentToOwner = "{0}Tk paid to {1} by {2}. Remaining Due: {3}Tk.";
    //public string SupplierAdvancePaymentToOwner = "{0}Tk paid to {1} as advance payment.";
    //public string ExpenseSmsToOwner = "{0}Tk has been paid as expense.";
    //public string IncomeSmsToOwner = "{0}Tk has been received as income.";
    //public string AdjustCustomerPaymentToOwner = "{0}Tk from {1} has been {2} as ledger balance adjustment. Current balance:{3}\n";
    //public string AdjustCustomerAdvancePaymentToOwner = "{0}Tk has been adjusted with sales due from {1} advance payment.";
    //public string AdjustSupplierAdvancePaymentToOwner = "{0}Tk has been adjusted with purchase due from {1} advance payment.";
    //public string LoanPaymentToOwner = "{0}Tk has been paid to {1} as loan payment.";
    //public string LoanReceivedToOwner = "{0}Tk has been received from {1} as loan received.";
    //public string transferAmountToOwner = "{0}Tk has been transfered from {1} to {2} successfully!.";
    //public string investmentAmountToOwner = "{0}Tk has been added in {1} as investment successfully!.";
    //public string withdrawAmountToOwner = "{0}Tk has been withdraw from {1} successfully!.";

    //public string sendOTP = "Your One-Time PIN is {0}.\nFrom\n{1}";


}
