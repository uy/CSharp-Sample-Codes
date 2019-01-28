class Password
    {
        string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        string PASSWORD_CHARS_NUMERIC = "0192837465";
        string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        int MIN_PASSWORD_LETTERS = 1;
        int MIN_PASSWORD_DIGITS = 1;
        int MIN_PASSWORD_UPPERCASE = 1; 
        int MIN_PASSWORD_LOWERCASE = 1;
        int MIN_PASSWORD_LNG = 8;
        int MIN_PASSWORD_SPECIALS = 1;

        public String GeneratePassword()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            int seed = BitConverter.ToInt32(randomBytes, 0);
            Random random = new Random(seed);

            int passwordLength = random.Next(MIN_PASSWORD_LNG, MIN_PASSWORD_LNG + 5);
            int remainPasswordChars = passwordLength;
            char[] password = new char[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = ' ';
            }

            // Letters
            if (MIN_PASSWORD_LETTERS > 0 && MIN_PASSWORD_UPPERCASE > 0)
            {
                for (int i = 0; i < MIN_PASSWORD_UPPERCASE; i++)
                {
                    remainPasswordChars--;
                    password[remainPasswordChars] = PASSWORD_CHARS_UCASE[random.Next(0, PASSWORD_CHARS_UCASE.Count())];
                }
            }
            if (MIN_PASSWORD_LETTERS > 0 && MIN_PASSWORD_LOWERCASE > 0)
            {
                for (int i = 0; i < MIN_PASSWORD_LOWERCASE; i++)
                {
                    remainPasswordChars--;
                    password[remainPasswordChars] = PASSWORD_CHARS_LCASE[random.Next(0, PASSWORD_CHARS_LCASE.Count())];
                }
            }

            // Digits
            if (MIN_PASSWORD_DIGITS > 0)
            {
                for (int i = 0; i < MIN_PASSWORD_DIGITS; i++)
                {
                    remainPasswordChars--;
                    password[remainPasswordChars] = PASSWORD_CHARS_NUMERIC[random.Next(0, PASSWORD_CHARS_NUMERIC.Count())];
                }
            }

            // Special Chars
            if (MIN_PASSWORD_SPECIALS > 0)
            {
                for (int i = 0; i < MIN_PASSWORD_SPECIALS; i++)
                {
                    remainPasswordChars--;
                    password[remainPasswordChars] = PASSWORD_CHARS_SPECIAL[random.Next(0, PASSWORD_CHARS_SPECIAL.Count())];
                }
            }

            // Filling empty chars
            if (remainPasswordChars > 0)
            {
                List<String> pools = new List<string>();
                if ((MIN_PASSWORD_LETTERS - (MIN_PASSWORD_UPPERCASE + MIN_PASSWORD_LOWERCASE)) > 0)
                {
                    pools.Add(PASSWORD_CHARS_UCASE);
                    pools.Add(PASSWORD_CHARS_LCASE);
                }
                if (MIN_PASSWORD_DIGITS > 0)
                {
                    pools.Add(PASSWORD_CHARS_NUMERIC);
                }
                if (MIN_PASSWORD_SPECIALS > 0)
                {
                    pools.Add(PASSWORD_CHARS_SPECIAL);
                }

                int freeCharCount = remainPasswordChars;
                for (int i = 0; i < freeCharCount; i++)
                {
                    remainPasswordChars--;
                    string poolString = pools[random.Next(0, pools.Count())];
                    password[remainPasswordChars] = poolString[random.Next(0, poolString.Count())];
                }
            }

            password = password.OrderBy(x => random.Next()).ToArray();

            string _password = new string(password);
            _password = _password.Replace(" ", string.Empty);
            return _password;
        }

        public String CheckPassword(String password)
        {
            if (password.Length < MIN_PASSWORD_LNG)
            {
                return "Password should have at least " + MIN_PASSWORD_LNG + " character(s).";
            }

            if (MIN_PASSWORD_DIGITS > 0)
            {
                int checkCount = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (PASSWORD_CHARS_NUMERIC.Contains(password[i]))
                    {
                        checkCount++;
                    }
                }

                if (checkCount < MIN_PASSWORD_DIGITS)
                {
                    return "Password should have at least " + MIN_PASSWORD_DIGITS + " digit(s).";
                }
            }

            if (MIN_PASSWORD_UPPERCASE > 0)
            {
                int checkCount = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (PASSWORD_CHARS_UCASE.Contains(password[i]))
                    {
                        checkCount++;
                    }
                }

                if (checkCount < MIN_PASSWORD_UPPERCASE)
                {
                    return "Password should have at least " + MIN_PASSWORD_UPPERCASE + " upper case letter(s).";
                }
            }

            if (MIN_PASSWORD_LOWERCASE > 0)
            {
                int checkCount = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (PASSWORD_CHARS_LCASE.Contains(password[i]))
                    {
                        checkCount++;
                    }
                }

                if (checkCount < MIN_PASSWORD_LOWERCASE)
                {
                    return "Password should have at least " + MIN_PASSWORD_LOWERCASE + " lower case letter(s).";
                }
            }

            if (MIN_PASSWORD_SPECIALS > 0)
            {
                int checkCount = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (PASSWORD_CHARS_SPECIAL.Contains(password[i]))
                    {
                        checkCount++;
                    }
                }

                if (checkCount < MIN_PASSWORD_SPECIALS)
                {
                    return "Password should have at least " + MIN_PASSWORD_SPECIALS + " special character(s).";
                }
            }

            return "";
        }
    }
