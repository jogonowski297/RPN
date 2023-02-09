using NUnit.Framework;
using System;
using RPNCalulator;

namespace RPNTest {
	[TestFixture]
	public class RPNTest {
		private RPN _sut;
		[SetUp]
		public void Setup() {
			_sut = new RPN();
		}
		[Test]
		public void CheckIfTestWorks() {
			Assert.Pass();
		}

		[Test]
		public void CheckIfCanCreateSut() {
			Assert.That(_sut, Is.Not.Null);
		}

		[Test]
		public void SingleDigitOneInputOneReturn() {
			var result = _sut.EvalRPN("1");

			Assert.That(result, Is.EqualTo(1));

		}
		[Test]
		public void SingleDigitOtherThenOneInputNumberReturn() {
			var result = _sut.EvalRPN("2");

			Assert.That(result, Is.EqualTo(2));

		}
		[Test]
		public void TwoDigitsNumberInputNumberReturn() {
			var result = _sut.EvalRPN("12");

			Assert.That(result, Is.EqualTo(12));

		}
		[Test]
		public void TwoNumbersGivenWithoutOperator_ThrowsExcepton() {
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("1 2"));

		}
		[Test]
		public void OperatorPlus_AddingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("1 2 +");

			Assert.That(result, Is.EqualTo(3));
		}
		[Test]
		public void OperatorTimes_AddingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("2 2 *");

			Assert.That(result, Is.EqualTo(4));
		}
		[Test]
		public void OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("1 2 -");

			Assert.That(result, Is.EqualTo(-1));
		}
		[Test]
		public void ComplexExpression() {
			var result = _sut.EvalRPN("15 7 1 1 + - / 3 * 2 1 1 + + -");

			Assert.That(result, Is.EqualTo(5));
		}

        [Test]
        public void DzieleniePrzezZero()
        {
            string exception = "Nie dziel przez zero";

            var ex = Assert.Throws<Exception>(() => _sut.EvalRPN("15 0 /"));

            Assert.AreEqual(exception, ex.Message);
        }

        [Test]
        public void PowFunc()
        {
            var result = _sut.EvalRPN("2 ^2");

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Silnia()
        {
            var result = _sut.EvalRPN("5 !");

            Assert.That(result, Is.EqualTo(120));
        }

        [Test]
        public void WartBezw()
        {
            var result = _sut.EvalRPN("1 2 - ||");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void IsBinarTest()
        {
            var result = _sut.EvalRPN("B101 B101 +");

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void IsDecimalTest()
        {
            var result = _sut.EvalRPN("D10 D10 +");

            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void IsHexTest()
        {
            var result = _sut.EvalRPN("#AB #AB +");

            Assert.That(result, Is.EqualTo(342));
        }

        [Test]
        public void IsHexAndBinTest()
        {
            var result = _sut.EvalRPN("#AB B101 +");

            Assert.That(result, Is.EqualTo(176));
        }

        [Test]
        public void IsMinusDecAndBinTest()
        {
            var result = _sut.EvalRPN("B101 D10 -");

            Assert.That(result, Is.EqualTo(-5));
        }

        [Test]
        public void IsMinusDecAndBinBezwzTest()
        {
            var result = _sut.EvalRPN("B101 D10 - ||");

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void PustyStos()
        {
            string exception = "Brak danych";

            var ex = Assert.Throws<Exception>(() => _sut.EvalRPN(""));

            Assert.AreEqual(exception, ex.Message);
        }


    }
}