namespace WebPortal.Shared
{
    public class Item
    {
        public int quantity { get; set; }
        public string model { get; set; }
        public string eta { get; set; }
        private string number { get; set; }
        public string description { get; set; }
        private bool invIssue { get; set; }

        public Item()
        {
            number = "";
            description = "";
            eta = "";
        }
        public Item(string number, string model, string description, int quantity, bool invIssue, string eta)
        {
            this.number = number;
            this.model = model;
            this.description = description;
            this.quantity = quantity;
            this.invIssue = invIssue;
            this.eta = eta;
        }

        public string GetNumber() { return number; }
        public string GetDescription() { return description; }
        public string GetETA() { return eta; }
        public string GetModel() { return model; }

        public string AsString()
        {
            return number + " " + description + " " + eta + " " + model + " " + quantity + " " + invIssue;
        }
    }
}