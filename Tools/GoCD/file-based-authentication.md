#GoCD specification
[File based authentication](https://docs.go.cd/16.1.0/configuration/dev_authentication.html#file-based-authentication)

#How to generate GoCD username and password
1. Go to [htpasswd generator - password encryption](http://aspirine.org/htpasswd_en.html)
2. Enter your `username` and `password`
3. Select `Sha-1`
4. Click `Generate htpasswd content`

The result for `username password` for example is `username:{SHA}W6ph5Mm5Pz8GgiULbPgzG37mj9g=`

![htpasswd generator example](https://raw.githubusercontent.com/Elders/be-a-better-dev/master/Tools/GoCD/htpasswd-generator-example.png)