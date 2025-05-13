            // Convert cookie name to lowercase for case-insensitive comparison
            string lowerCaseName = cookieName.ToLower();

            // Check for potential sensitive information exposure
            if (lowerCaseName.Contains("username") ||
                lowerCaseName.Contains("user") ||
                lowerCaseName.Contains("usr_") ||
                lowerCaseName.Contains("login") ||
                lowerCaseName.Contains("email"))
            {
                return "Sensitive Information Exposure: The cookie name indicates it might store sensitive information such as a username or email address. Storing such data in cookies poses a significant risk. If an attacker is able to intercept or access the cookies, they could potentially retrieve user credentials or email addresses. This could lead to unauthorized access to user accounts, identity theft, or targeted phishing attacks. It is crucial to avoid storing sensitive information in cookies and instead manage user identities securely on the server side.";
            }

            if (lowerCaseName.Contains("password") ||
                lowerCaseName.Contains("pass") ||
                lowerCaseName.Contains("pwd") ||
                lowerCaseName.Contains("auth"))
            {
                return "Sensitive Information Exposure: The cookie name suggests it may contain password information or authentication tokens. If passwords or authentication data are stored in cookies, they could be exposed if the cookies are intercepted by an attacker. This exposure could lead to unauthorized access to user accounts, compromising the security of personal or organizational data. Cookies storing sensitive information should always be secured with encryption and transmitted over secure channels. Additionally, using secure server-side sessions for authentication is highly recommended.";
            }

            // Check for session management vulnerabilities
            if (lowerCaseName.Contains("session") &&
                !lowerCaseName.Contains("id") ||
                lowerCaseName.Contains("ssid") ||
                lowerCaseName.Contains("sid") ||
                lowerCaseName.Contains("sessionid"))
            {
                return "Session Management Vulnerability: The cookie name indicates that it may be used for session management but does not follow best practices for naming, such as including 'id'. This could imply that the session management mechanism may not be following industry-standard practices. Poor session management could lead to session fixation attacks, where an attacker sets a known session ID to hijack a session. It is important to use unique, unpredictable session IDs and to follow best practices for session management to prevent unauthorized access.";
            }
            // Add checks for BrowserId or similar identifiers
            if (lowerCaseName.Contains("browserid") ||
                lowerCaseName.Contains("bid") ||
                lowerCaseName.Contains("browser"))
            {
                return "Sensitive Identifier Exposure. The cookie contains an identifier such as 'BrowserId' or similar (e.g., 'bid', 'session_browser'), which can be used to track or associate a session with a particular user or device. If these identifiers are stored in plain text and intercepted during transmission, they could expose crucial session information. Attackers can leverage this data to impersonate the user by hijacking their session, allowing unauthorized access to the system. This is particularly dangerous in scenarios where session information is used to maintain user authentication or persist sensitive operations across pages. Additionally, man-in-the-middle (MITM) attacks can exploit this information by capturing these cookies in transit, especially if the communication is not encrypted (e.g., over HTTP instead of HTTPS). Once exposed, the attacker could use this data to reconstruct the session and assume control over the users account without needing credentials.";
            }          
            if (lowerCaseName.Contains("cart") || lowerCaseName.Contains("checkout"))
            {
                return "Transaction Data Exposure. Cookies related to 'cart' or 'checkout' could expose sensitive transaction data, leading to unauthorized accesTransaction Data Exposure. Cookies with names related to 'cart' or 'checkout' may indicate they store sensitive transaction-related data, such as items in the user's shopping cart, billing information, or purchase preferences. If these cookies are not properly secured, an attacker who intercepts or gains access to them could retrieve this sensitive data. This exposure could allow unauthorized parties to view or manipulate a user's purchase information, potentially leading to financial fraud, unauthorized orders, or privacy violations. Furthermore, if this data includes personally identifiable information (PII) like customer IDs, addresses, or payment details, it significantly heightens the risk of identity theft, data breaches, and compliance violations (such as GDPR or PCI DSS). Ensuring that sensitive transaction data is obfuscated or encrypted within cookies is crucial to preventing such attacks.s or privacy breaches.";
            }
            if (lowerCaseName.Contains("remember") || lowerCaseName.Contains("rememberme"))
            {
                return "Persistent Login Exposure. 'Remember Me' cookies are typically used to retain a users login state across sessions, allowing them to remain logged into a website or application even after closing their browser. While convenient for users, these cookies can persist for extended periods, often far beyond the users intention, increasing the risk of unauthorized access if not properly managed. If these cookies are stored without sufficient security measures, such as proper encryption, attackers who gain access to the users device or intercept these cookies can potentially reuse them to bypass the authentication process. This is particularly risky if the cookie contains long-lived authentication tokens or credentials that allow an attacker to impersonate the user on the platform, granting them full access to the account without requiring a password.";
            }


            if (lowerCaseName.Contains("otp") || lowerCaseName.Contains("2fa"))
            {
                return "2FA/OTP Exposure. The cookie may store sensitive information related to two-factor authentication (2FA) or one-time passwords (OTP), which are typically used to provide an additional layer of security during user authentication. If these cookies are intercepted, either through network sniffing (e.g., via a man-in-the-middle attack) or cross-site scripting (XSS), an attacker could gain access to the OTP or 2FA token. This would allow them to bypass the second layer of authentication, effectively nullifying the security benefits of two-factor authentication.Since OTPs and 2FA tokens are designed to be temporary and valid for a short period, storing them in cookies even for brief intervals poses a serious risk. An attacker who intercepts these cookies during their valid time window could potentially authenticate as the user without needing the users primary credentials. Furthermore, once the 2FA or OTP token is compromised, it undermines the entire security framework of the application, granting unauthorized access to sensitive accounts or resources.";
            }


            if (lowerCaseName.Contains("admin") || lowerCaseName.Contains("administrator"))
            {
                return "Administrative Privilege Exposure. Cookies associated with administrative privileges could contain sensitive information, such as session tokens or authentication data, that grants elevated access to critical system functions. If these cookies are intercepted, an attacker could potentially gain unauthorized access to the administrative interface of an application or system. This would allow the attacker to perform high-privilege actions, such as modifying user accounts, accessing sensitive data, or altering system configurations.";
            }


            if (lowerCaseName.Contains("tracking") || lowerCaseName.Contains("track"))
            {
                return "Tracking Data Exposure. Cookies used for tracking purposes such as those associated with user activity, browsing history, or behavior across websites can pose a significant privacy risk if intercepted. These cookies often contain detailed information about a user's online interactions, including visited pages, search queries, time spent on websites, and sometimes even geolocation data. If such tracking cookies are exposed, an attacker could compile a comprehensive profile of the user's behavior, preferences, and private information without their consent.";
            }


            if (lowerCaseName.Contains("lang") || lowerCaseName.Contains("locale"))
            {
                return "Language/Locale Exposure. Cookies storing language or locale preferences may not seem critical, but if exposed, they could reveal private user information or contribute to tracking.";
            }

            if (lowerCaseName.Contains("apikey") || lowerCaseName.Contains("appkey"))
            {
                return "API Key Exposure. API keys are stored in cookies, making them vulnerable to interception if the cookies are not properly secured. API keys are sensitive credentials used to authenticate and authorize requests to backend services. If an attacker intercepts these cookies, they can use the exposed API keys to gain unauthorized access to services or data, potentially leading to data breaches, unauthorized actions, or service disruptions.";
            }

            if (lowerCaseName.Contains("preferences"))
            {
                return "User Preferences Exposure. Storing user preferences in cookies can pose serious privacy risks if those cookies are intercepted. For example, a web application that saves detailed user preferences in cookies, including visited pages, search history, or sensitive settings related to personal topics like health or financial matters. If an attacker gains access to these cookies, they can compile a detailed profile of the user's behavior and private interests. In a notable case, attackers leveraged cookie-stored preferences to create highly targeted phishing attacks. By analyzing the data, they crafted convincing fraudulent messages aligned with the user's interests, leading to significant financial losses and compromised personal information. This illustrates the critical need for secure handling of user preference data to protect against such malicious activities.";
            }

            if (lowerCaseName.Contains("referer") || lowerCaseName.Contains("ref"))
            {
                return "User Referral Exposure. Storing referer or referral information in cookies can lead to the unintended exposure of sensitive navigation history, which may be exploited by attackers. For instance, if a web application saves referer information—such as the URLs of previous pages or external sites visited—into cookies, this data can provide insights into a user's browsing behavior and interactions. If intercepted, this information can be leveraged for targeted phishing or social engineering attacks. Attackers could use detailed navigation history to craft convincing fraudulent messages or impersonate trusted entities, increasing the likelihood of successful attacks and potentially leading to unauthorized access to personal or corporate information. This underscores the importance of securing referer data and ensuring it is not stored in cookies to prevent misuse and protect user privacy.";
            }

            if (lowerCaseName.Contains("payment") || lowerCaseName.Contains("billing"))
            {
                return "Payment Information Exposure. Cookies with 'payment' or 'billing' data could lead to the exposure of sensitive financial information if intercepted.";
            }

            if (lowerCaseName.Contains("subid") || lowerCaseName.Contains("affiliate"))
            {
                return "Affiliate Tracking Data Exposure. Cookies related to affiliate tracking could expose sensitive affiliate information that could be exploited by attackers.";
            }

            if (lowerCaseName.Contains("device") || lowerCaseName.Contains("dev"))
            {
                return "Device Information Exposure. Cookies containing device-related information may expose details about the user's device that can be exploited in targeted attacks.";
            }

            if (lowerCaseName.Contains("geo") || lowerCaseName.Contains("location"))
            {
                return "Geolocation Data Exposure. Cookies storing geolocation or location data can expose sensitive information about the user's physical location if intercepted.";
            }

            if (lowerCaseName.Contains("auth") || lowerCaseName.Contains("sso"))
            {
                return "Single Sign-On (SSO) Exposure. Cookies associated with Single Sign-On (SSO) authentication represent a critical security risk if intercepted. SSO cookies often contain highly sensitive authentication tokens that provide seamless access to multiple systems or applications with a single set of credentials. If these cookies are compromised, attackers can gain unauthorized access to a wide range of systems and sensitive data without needing additional credentials.";
            }

            // Return an empty string if no vulnerabilities are detected
            return string.Empty;





























            private string AnalyzeCookie(string cookieName, string cookieValue)
            {
                // Convert cookie name to lowercase for case-insensitive comparison
                string lowerCaseName = cookieName.ToLower();
            
                // Check for potential sensitive information exposure
                if (lowerCaseName.Contains("username") ||
                    lowerCaseName.Contains("user") ||
                    lowerCaseName.Contains("usr_") ||
                    lowerCaseName.Contains("login") ||
                    lowerCaseName.Contains("email"))
                {
                    return "Vulnerability: Sensitive Information Exposure. The cookie name indicates it may store sensitive information such as usernames or email addresses, which could be exposed if intercepted by an attacker. This can lead to unauthorized access or phishing attacks.";
                }
            
                if (lowerCaseName.Contains("password") ||
                    lowerCaseName.Contains("pass") ||
                    lowerCaseName.Contains("pwd") ||
                    lowerCaseName.Contains("auth"))
                {
                    return "Vulnerability: Sensitive Information Exposure. The cookie may contain passwords or authentication tokens, posing a risk of unauthorized access if intercepted.";
                }
            
                // Check for session management vulnerabilities
                if (lowerCaseName.Contains("session") &&
                    !lowerCaseName.Contains("id"))
                {
                    return "Vulnerability: Session Management. The cookie name suggests improper session management, potentially leading to session fixation attacks.";
                }
            
                // Add checks for BrowserId or similar identifiers
                if (lowerCaseName.Contains("browserid") ||
                    lowerCaseName.Contains("bid") ||
                    lowerCaseName.Contains("browser"))
                {
                    return "Vulnerability: Sensitive Identifier Exposure. The cookie contains 'BrowserId' or similar identifiers, which could expose session details, increasing the risk of session hijacking or man-in-the-middle attacks.";
                }
            
                // Check for security flags
                if (lowerCaseName.Contains("secure") ||
                    lowerCaseName.Contains("__secure") ||
                    lowerCaseName.Contains("__Secure"))
                {
                    return "Vulnerability: Incomplete Security. While the cookie name suggests security (e.g., 'secure'), there is no guarantee that proper security mechanisms (like the 'Secure' flag) are enforced.";
                }
            
                // Check for session ID-related vulnerabilities
                if (lowerCaseName.Contains("sid") ||
                    lowerCaseName.Contains("sessionid"))
                {
                    return "Vulnerability: Session ID Exposure. The cookie may expose session IDs, which can be intercepted and lead to session hijacking.";
                }
            
                if (lowerCaseName.Contains("cart") || lowerCaseName.Contains("checkout"))
                {
                    return "Vulnerability: Transaction Data Exposure. Cookies related to 'cart' or 'checkout' could expose sensitive transaction data, leading to unauthorized access or privacy breaches.";
                }
            
                if (lowerCaseName.Contains("token") || lowerCaseName.Contains("jwt"))
                {
                    return "Vulnerability: Authentication Token Exposure. The cookie may store authentication tokens (like JWTs), which can be exploited if intercepted.";
                }
            
                if (lowerCaseName.Contains("remember") || lowerCaseName.Contains("rememberme"))
                {
                    return "Vulnerability: Persistent Login Exposure. 'Remember Me' cookies can persist beyond intended use, posing a risk of unauthorized access.";
                }
            
                if (lowerCaseName.Contains("otp") || lowerCaseName.Contains("2fa"))
                {
                    return "Vulnerability: 2FA/OTP Exposure. The cookie may expose one-time passwords or 2FA tokens, which, if intercepted, could compromise two-factor authentication mechanisms.";
                }
            
                // Check for cookie expiry-related vulnerabilities
                if (lowerCaseName.Contains("expires") || lowerCaseName.Contains("ttl"))
                {
                    return "Vulnerability: Expiry Mismanagement. Inadequate expiration settings could leave sensitive data accessible for longer than intended.";
                }
            
                if (lowerCaseName.Contains("admin") || lowerCaseName.Contains("administrator"))
                {
                    return "Vulnerability: Administrative Privilege Exposure. Cookies with administrative privileges may expose sensitive access if intercepted.";
                }
            
                if (lowerCaseName.Contains("tracking") || lowerCaseName.Contains("track"))
                {
                    return "Vulnerability: Tracking Data Exposure. Cookies used for tracking can be exploited to reveal user behavior or private data if intercepted.";
                }
            
                // New vulnerability checks
                if (lowerCaseName.Contains("lang") || lowerCaseName.Contains("locale"))
                {
                    return "Vulnerability: Language/Locale Exposure. Cookies storing language or locale preferences may not seem critical, but if exposed, they could reveal private user information or contribute to tracking.";
                }
            
                if (lowerCaseName.Contains("promo") || lowerCaseName.Contains("discount"))
                {
                    return "Vulnerability: Promotion Code Exposure. Cookies with promo or discount data could expose sensitive promotional information that may be exploited by attackers to gain unauthorized discounts.";
                }
            
                if (lowerCaseName.Contains("preferences"))
                {
                    return "Vulnerability: User Preferences Exposure. Storing user preferences in cookies could expose details about user behavior and settings if intercepted.";
                }
            
                if (lowerCaseName.Contains("apikey") || lowerCaseName.Contains("appkey"))
                {
                    return "Vulnerability: API Key Exposure. Storing API keys in cookies can lead to unauthorized access to backend services if the cookie is intercepted.";
                }
            
                if (lowerCaseName.Contains("referer") || lowerCaseName.Contains("ref"))
                {
                    return "Vulnerability: Referer Data Exposure. Storing referer or referral information in cookies may expose sensitive navigation history that can be exploited for phishing or social engineering attacks.";
                }
            
                if (lowerCaseName.Contains("payment") || lowerCaseName.Contains("billing"))
                {
                    return "Vulnerability: Payment Information Exposure. Cookies with 'payment' or 'billing' data could lead to the exposure of sensitive financial information if intercepted.";
                }
            
                if (lowerCaseName.Contains("subid") || lowerCaseName.Contains("affiliate"))
                {
                    return "Vulnerability: Affiliate Tracking Data Exposure. Cookies related to affiliate tracking could expose sensitive affiliate information that could be exploited by attackers.";
                }
            
                if (lowerCaseName.Contains("device") || lowerCaseName.Contains("dev"))
                {
                    return "Vulnerability: Device Information Exposure. Cookies containing device-related information may expose details about the user's device that can be exploited in targeted attacks.";
                }
            
                if (lowerCaseName.Contains("geo") || lowerCaseName.Contains("location"))
                {
                    return "Vulnerability: Geolocation Data Exposure. Cookies storing geolocation or location data can expose sensitive information about the user's physical location if intercepted.";
                }
            
                if (lowerCaseName.Contains("auth") || lowerCaseName.Contains("sso"))
                {
                    return "Vulnerability: Single Sign-On (SSO) Exposure. Cookies related to SSO authentication could expose sensitive authentication tokens if intercepted, leading to unauthorized access.";
                }
            
                // Return an empty string if no vulnerabilities are detected
                return string.Empty;
            }
            