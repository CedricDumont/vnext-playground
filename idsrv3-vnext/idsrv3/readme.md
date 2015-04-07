Identityserver v3 install using vnext.

This repo is the code of this stack overflow question : 
http://stackoverflow.com/questions/29360563/asp-net-5-oauth-bearer-token-authentication/29487480#29487480

to make the code build, I use this nuget feed to get the packages (so add them to your nuget package feed if you want a restore ) as some of the vnext lib are still in dev (07/04/2015)

nuget feed to use : https://www.myget.org/F/aspnetvnext/

To test it, just hit F5 and it should run. Identity server is configure to this URL  http://localhost:4369/core
the test controller listen to request here : 

To test with fiddler : use the composer.

1/ first try to hit the controller:
------------------------------------------------------------------

		GET http://localhost:4369/api/Test

		you should get a : HTTP/1.1 401 Unauthorized

				
------------------------------------------------------------------

2/ next try to get an access_token

------------------------------------------------------------------

		POST : http://localhost:4369/core/connect/token


		User-Agent: Fiddler
		Content-Length: 67
		Content-Type: application/x-www-form-urlencoded 
		Authorization: Basic SWRlbnRpdHlXZWJVSTpzZWNyZXQ=
		Host: localhost:4369

		grant_type=password&username=testUser&password=testPwd&scope=openid

------------------------------------------------------------------

		response should be sthg like :
		 
		HTTP/1.1 200 OK
		{"access_token":"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJJZGVudGl0eVdlYlVJIiwic2NvcGUiOiJvcGVuaWQiLCJzdWIiOiJJIGFtIHRoZSBTdWJqZWN0IiwiYW1yIjoicGFzc3dvcmQiLCJhdXRoX3RpbWUiOjE0MjgzOTk1NDAsImlkcCI6Imlkc3J2IiwiaXNzIjoiaHR0cHM6Ly9pZHNydjMuY29tIiwiYXVkIjoiaHR0cHM6Ly9pZHNydjMuY29tL3Jlc291cmNlcyIsImV4cCI6MTQyODQwMzE0MCwibmJmIjoxNDI4Mzk5NTQwfQ.KtW4uZbTVGKyvX3NraaAS5M5uPTyhrFa5pRccWV8XkvHooWip2f0JdSqPJRQGrtwEuro7bsu0FqAtzwvaxTiV3lHPtPBb8fSQgjYdtwkpLy2CvCQnObB1NaVthuefORDdGFnsPcL24L6F-LiIbvSVoxV_qtZxG-_xOMfys256cTjETMH5-UToHoLOLWm3WI4C1SrhbtC14as5lKr-JSr4vvUvWCezktSaL0WVlcINWzUXxaTnChNu_pUWkRH9dEAfP-ZggeGdeVgpthXZm3on2JyZtfd8Nk024EmBlN5K4i_ed015FGFgwJKL1FlXHxRo1H5KyVHnv_ghmn02P2xOw","expires_in":3600,"token_type":"Bearer"}

------------------------------------------------------------------

the access_token is : eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJJZGVudGl0eVdlYlVJIiwic2NvcGUiOiJvcGVuaWQiLCJzdWIiOiJJIGFtIHRoZSBTdWJqZWN0IiwiYW1yIjoicGFzc3dvcmQiLCJhdXRoX3RpbWUiOjE0MjgzOTk1NDAsImlkcCI6Imlkc3J2IiwiaXNzIjoiaHR0cHM6Ly9pZHNydjMuY29tIiwiYXVkIjoiaHR0cHM6Ly9pZHNydjMuY29tL3Jlc291cmNlcyIsImV4cCI6MTQyODQwMzE0MCwibmJmIjoxNDI4Mzk5NTQwfQ.KtW4uZbTVGKyvX3NraaAS5M5uPTyhrFa5pRccWV8XkvHooWip2f0JdSqPJRQGrtwEuro7bsu0FqAtzwvaxTiV3lHPtPBb8fSQgjYdtwkpLy2CvCQnObB1NaVthuefORDdGFnsPcL24L6F-LiIbvSVoxV_qtZxG-_xOMfys256cTjETMH5-UToHoLOLWm3WI4C1SrhbtC14as5lKr-JSr4vvUvWCezktSaL0WVlcINWzUXxaTnChNu_pUWkRH9dEAfP-ZggeGdeVgpthXZm3on2JyZtfd8Nk024EmBlN5K4i_ed015FGFgwJKL1FlXHxRo1H5KyVHnv_ghmn02P2xOw



3/ retry the first request but add the Bearer token
------------------------------------------------------------------

		GET http://localhost:4369/api/Test

		User-Agent: Fiddler
		Host: localhost:4369
		Content-Length: 0
		Content-Type: application/x-www-form-urlencoded
		Authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSIsImtpZCI6ImEzck1VZ01Gdjl0UGNsTGE2eUYzekFrZnF1RSJ9.eyJjbGllbnRfaWQiOiJJZGVudGl0eVdlYlVJIiwic2NvcGUiOiJvcGVuaWQiLCJzdWIiOiJJIGFtIHRoZSBTdWJqZWN0IiwiYW1yIjoicGFzc3dvcmQiLCJhdXRoX3RpbWUiOjE0MjgzOTk1NDAsImlkcCI6Imlkc3J2IiwiaXNzIjoiaHR0cHM6Ly9pZHNydjMuY29tIiwiYXVkIjoiaHR0cHM6Ly9pZHNydjMuY29tL3Jlc291cmNlcyIsImV4cCI6MTQyODQwMzE0MCwibmJmIjoxNDI4Mzk5NTQwfQ.KtW4uZbTVGKyvX3NraaAS5M5uPTyhrFa5pRccWV8XkvHooWip2f0JdSqPJRQGrtwEuro7bsu0FqAtzwvaxTiV3lHPtPBb8fSQgjYdtwkpLy2CvCQnObB1NaVthuefORDdGFnsPcL24L6F-LiIbvSVoxV_qtZxG-_xOMfys256cTjETMH5-UToHoLOLWm3WI4C1SrhbtC14as5lKr-JSr4vvUvWCezktSaL0WVlcINWzUXxaTnChNu_pUWkRH9dEAfP-ZggeGdeVgpthXZm3on2JyZtfd8Nk024EmBlN5K4i_ed015FGFgwJKL1FlXHxRo1H5KyVHnv_ghmn02P2xOw


------------------------------------------------------------------

		the response should be :

		HTTP/1.1 200 OK
		Content-Type: application/json; charset=utf-8
		Server: Microsoft-IIS/10.0
		X-SourceFiles: =?UTF-8?B?QzpcX3RlbXBcR2l0SHViXHZuZXh0LXBsYXlncm91bmRcaWRzcnYzLXZuZXh0XGlkc3J2M1xzcmNcaWRzcnYzXHd3d3Jvb3RcYXBpXFRlc3Q=?=
		X-Powered-By: ASP.NET
		Date: Tue, 07 Apr 2015 09:42:19 GMT
		Content-Length: 54

		{"message":"You See this then it's ok auth is  :True"}

And voilà

