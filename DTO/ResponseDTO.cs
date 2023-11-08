namespace TheatreBookingApp;

public class ResponseDTO{
    public object ? Data{get;set;}
    public Response Status{get;set;}
    public String Message {get;set;}
}
public enum Response{
    sucess,
    error
}