using System;
using System.Collections.Generic;
using System.Text;

namespace Itenium.Interview
{
    public static class PhoneInvoicing
    {
        public static void HandleInvoices(IEnumerable subscriptions)
        {
            foreach (var subscription in subscriptions)
            {
                if (subscription is PersonalSubscription personalSub)
                {
                    var totalCost = GetTotalCost(personalSub);
                    CreatePersonalInvoice(personalSub, totalCost);
                    Notify(personalSub, totalCost);
                }
                else if (subscription is BusinessSubscription bussinessSub)
                {
                    var totalCost = GetTotalCost(bussinessSub);
                    CreatePersonalInvoice(bussinessSub, totalCost);
                    BusinessPortal.AddInvoice(bussinessSub);
                }
                else if (subscription is FamilySubscription familySub)
                {
                    var totalCost = GetTotalCost(familySub);
                    CreateFamilyInvoice(familySub, totalCost);
                    Notify(familySub, totalCost);
                }
            }
        }

		public static decimal GetTotalCost(object subscription)
        {
            if (subscription is PersonalSubscription personalSub)
                return personalSub.TotalCost;

            if (subscription is BusinessSubscription bussinessSub)
                return bussinessSub.CalculateTotalCost();

            if (subscription is FamilySubscription familySub)
                return familySub.GetCostForFamily();
        }

        public static void Notify(object subscription, decimal totalAmount)
        {
            if (subscription is PersonalSubscription personalSub)
                EmailService.SendInvoicingEmail(personalSub, totalAmount);

            if (subscription is FamilySubscription familySub)
                EmailService.SendInvoicingEmail(familySub, totalAmount);
        }
    }

	public class PersonalSubscription
    {
        private const decimal CostPerSimCard = 25;
        private readonly int _amountOfSimCards;

        public decimal TotalCost => CostPerSimCard * _amountOfSimCards;

        public PersonalSubscription(int amountOfSimCards)
        {
            _amountOfSimCards = amountOfSimCards;
        }
    }

	public class BusinessSubscription
    {
        private const decimal CostPerSimCard = 10;
        private readonly int _amountOfSimCards;

        public BusinessSubscription(int amountOfSimCards)
        {
            _amountOfSimCards = amountOfSimCards;
        }

		public decimal CalculateTotalCost()
        {
            if (_amountOfSimCards < 50)
                return _amountOfSimCards * CostPerSimCard;

            return _amountOfSimCards * CostPerSimCard * 0.75;
        }
    }

	public class FamilySubscription
    {
        private const decimal CostFirstSimCard = 25;
        private const decimal CostPerAdditionalSimCard = 10;
        private readonly int _amountOfSimCards;

        public FamilySubscription(int amountOfSimCards)
        {
            _amountOfSimCards = amountOfSimCards;
        }

		public decimal GetCostForFamily()
        {
            return CostFirstSimCard + (_amountOfSimCards - 1) * CostPerAdditionalSimCard;
        }
    }
}
