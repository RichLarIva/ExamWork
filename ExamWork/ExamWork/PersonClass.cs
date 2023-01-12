using System;
namespace ExamWork
{
	public class PersonClass
	{
		private string _fullName;
		private string _barcode;
		private bool _isFacualty;

		public string FullName { get => _fullName; set => _fullName = value; }
		public string Barcode { get => _barcode; set => _barcode = value; }
		public bool IsFacualty { get => _isFacualty; set => _isFacualty = value; }
	}
}

