using System.Windows.Input;
namespace AppMobile;

public class MainViewModel : BindableObject
{
    double FirstNumber=0;
    double SecondNumber;
    string SelectedOperator;
    bool ongoingOP=false;//nous dit si une operation est en cours ou pas
    bool IsNewEntry=true ;//nous dit si on est pret a taper un nouveau nombre ou pas
    public string DisplayText{get;set;}="0";
    public string History {get;set;}
    public ICommand DigitPressed {get;}
    public ICommand OpePressed{get;}
    public ICommand EqualPressed{get;}
    public ICommand ClearPressed{get;}
    
    public MainViewModel(){
        DigitPressed= new Command<string>(OnDigit);
        OpePressed= new Command<string>(OPress);
        EqualPressed= new Command(OnEqual);
        ClearPressed= new Command(OnClear);
    }
    private void OnClear(){
        DisplayText="0";
        FirstNumber=0;
        IsNewEntry=true;
        OnPropertyChanged(nameof(DisplayText));
        History="";
            OnPropertyChanged(nameof(History));
    }
    private void OnEqual(){
        SecondNumber=double.Parse(DisplayText);
        FirstNumber=Calculate(FirstNumber,SecondNumber,SelectedOperator);
        DisplayText= FirstNumber.ToString();
        OnPropertyChanged(nameof(DisplayText));
        IsNewEntry=true;
        ongoingOP=false;
    }
    private void OPress(string op){
        if(ongoingOP==false){
        SelectedOperator=op;
        FirstNumber= double.Parse(DisplayText);
        ongoingOP=true;
        IsNewEntry=true;
        History += op;
            OnPropertyChanged(nameof(History));}
        else{
            if (IsNewEntry){
                SelectedOperator=op;

            }
            else{
               
                SecondNumber=double.Parse(DisplayText);
                FirstNumber=Calculate(FirstNumber,SecondNumber,SelectedOperator);
                DisplayText= FirstNumber.ToString();
                OnPropertyChanged(nameof(DisplayText));
                IsNewEntry=true;
                 SelectedOperator=op;
            }
        }
    }
    private double Calculate(double FirstNumber,double SecondNumber,string op){
        switch(SelectedOperator){
            case "+":
                return FirstNumber+SecondNumber;
            case "*":
                return FirstNumber*SecondNumber;
            case "-":
                return FirstNumber-SecondNumber;
            case "/":
                return SecondNumber!= 0 ? FirstNumber/SecondNumber : 0;
            default:
                return double.Parse(DisplayText);
        }
    }
    private void OnDigit(string digit){
        if (IsNewEntry){
            DisplayText=digit;
            IsNewEntry=false;
            OnPropertyChanged(nameof(DisplayText));
            History += digit;
            OnPropertyChanged(nameof(History));
        }
        else {
            DisplayText += digit;
            History += digit;
            OnPropertyChanged(nameof(History));
            OnPropertyChanged(nameof(DisplayText));
            
        }
    }
}