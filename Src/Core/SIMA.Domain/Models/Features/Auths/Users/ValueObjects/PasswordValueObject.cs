using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Sima.Framework.Core.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;
using System.Security.Cryptography;

namespace SIMA.Domain.Models.Features.Auths.Users.ValueObjects
{
    public class PasswordValueObject : ValueObject
    {
        public string Password { get; private set; }
        public string SecretKey { get; private set; }
        private PasswordValueObject()
        { }
        private PasswordValueObject(string password, string salt)
        {
            Password = password;
            SecretKey = salt;
        }
        public static PasswordValueObject New(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashedPassword = CalculateHashedValue(password, salt);
            var passwordString = Convert.ToBase64String(hashedPassword);
            var saltString = Convert.ToBase64String(salt);
            return new PasswordValueObject(passwordString, saltString);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Password;
            yield return SecretKey;
        }

        public void Verify(string textToCheck)
        {
            var byteSalt = Convert.FromBase64String(SecretKey);
            var hashedValue = CalculateHashedValue(textToCheck, byteSalt);
            var expectedHashBytes = Convert.FromBase64String(Password);
            GuardAgainstPasswordEquality(hashedValue, expectedHashBytes);
        }
        private void GuardAgainstPasswordEquality(byte[] hashedValue, byte[] expectedHashBytes)
        {
            var isEqual = hashedValue.SequenceEqual(expectedHashBytes);
            if (!isEqual)
                throw new SimaResultException("10002",Messages.InvalidUsernameOrPasswordError)
                    ;
        }

        private static byte[] CalculateHashedValue(string plainText, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: plainText,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
        }
    }
}
