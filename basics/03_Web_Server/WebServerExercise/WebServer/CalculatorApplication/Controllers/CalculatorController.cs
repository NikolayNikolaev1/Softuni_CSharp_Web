namespace WebServer.CalculatorApplication.Controllers
{
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using System.IO;
    using Views;

    public class CalculatorController
    {
        private string DefaultPath = @"../../../CalculatorApplication/Resources/Html/calculator.html";

        public IHttpResponse Index()
        {

            string htmlFile = File.ReadAllText(DefaultPath)
                .Replace(@"{{{display}}}", "none");
            return new ViewResponse(HttpResponseStatusCode.OK, new CalculatorIndexView(htmlFile));
        }

        public IHttpResponse Calculate(string firstNumber, string secondNumber, string sign)
        {
            string htmlFile = File.ReadAllText(DefaultPath);
            double result = 0.0;
            double firstNum = double.Parse(firstNumber);
            double secondNum = double.Parse(secondNumber);
            switch (sign)
            {
                case "+":
                    result = firstNum + secondNum;
                    break;
                case "-":
                    result = firstNum - secondNum;
                    break;
                case "*":
                    result = firstNum * secondNum;
                    break;
                case "/":
                    result = firstNum / secondNum;
                    break;
                default:
                    string invalidSignHtml = htmlFile
                        .Replace(@"{{{result}}}", "Invalid sign")
                        .Replace(@"{{{display}}}", "block");
                    return new ViewResponse(HttpResponseStatusCode.OK, new CalculatorIndexView(invalidSignHtml));
            }


            string resultHtml = htmlFile.Replace(@"{{{result}}}", $"Result: {result}");


            return new ViewResponse(HttpResponseStatusCode.OK, new CalculatorIndexView(resultHtml));
        }
    }
}
