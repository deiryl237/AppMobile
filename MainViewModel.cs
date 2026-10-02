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
    public ICommand BackSpace{get;}
    
    public MainViewModel(){
        DigitPressed= new Command<string>(OnDigit);
        OpePressed= new Command<string>(OPress);
        EqualPressed= new Command(OnEqual);
        ClearPressed= new Command(OnClear);
        BackSpace= new Command(Back);
    }
    private void Back(){
        if (DisplayText != "0"){
            if (DisplayText.Length > 1){
                if (ongoingOP){
            History=History.Remove(History.Length -1);
            OnPropertyChanged(nameof(History));
            DisplayText=DisplayText.Remove(DisplayText.Length -1);
            OnPropertyChanged(nameof(DisplayText));}
            else{
                DisplayText=DisplayText.Remove(DisplayText.Length -1);
            OnPropertyChanged(nameof(DisplayText));
            }}
            else{
            if(ongoingOP){
            History=History.Remove(History.Length -1);
            OnPropertyChanged(nameof(History));
            DisplayText="0";
            OnPropertyChanged(nameof(DisplayText));
            IsNewEntry=true;}
            else{
                DisplayText="0";
            OnPropertyChanged(nameof(DisplayText));
            IsNewEntry=true;
            }
            }
        }
        else{
            IsNewEntry=true;
        }
    }
    private void OnClear(){
        DisplayText="0";
        FirstNumber=0;
        SecondNumber=0;
        SelectedOperator="";
        ongoingOP=false;
        IsNewEntry=true;
        OnPropertyChanged(nameof(DisplayText));
        History="";
            OnPropertyChanged(nameof(History));
    }
    private void OnEqual(){
        SecondNumber=double.Parse(DisplayText);
        if (SelectedOperator == "/" && SecondNumber == 0)
    {
        DisplayText = "Error, division by 0";
        OnPropertyChanged(nameof(DisplayText));
        IsNewEntry = true;
        ongoingOP = false;
        return; 
    }
        FirstNumber=Calculate(FirstNumber,SecondNumber,SelectedOperator);
        DisplayText= FirstNumber.ToString();
        OnPropertyChanged(nameof(DisplayText));
        IsNewEntry=true;
        ongoingOP=false;
    }
    private void OPress(string op){
        if(ongoingOP==false){
            if (DisplayText== "0" && op!= "-" ){
                SelectedOperator="";
                ongoingOP=false;
                IsNewEntry=true;
            }
            else{
        SelectedOperator=op;
        FirstNumber= double.Parse(DisplayText);
        ongoingOP=true;
        IsNewEntry=true;
        History += op;
        OnPropertyChanged(nameof(History));}}
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
                History += op;
                OnPropertyChanged(nameof(History));
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
            case "%":
                return (FirstNumber*100)/SecondNumber;
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