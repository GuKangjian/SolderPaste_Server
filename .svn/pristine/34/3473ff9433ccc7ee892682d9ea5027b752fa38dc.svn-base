#!/bin/sh
set -e -u

CF_API=${CF_API-"https://
api.sys.bosch-iot-cloud.com"}
CF_DOMAIN=${CF_DOMAIN-"apps.bosch-iot-cloud.com"}
CF_USER=${CF_USER-""}
CF_PASSWORD=${CF_PASSWORD-""}
CF_ORG=${CF_ORG-"BICS_Billing"}
CF_SPACE=${CF_SPACE-"development"}
CF_APPNAME=${CF_APPNAME-""}
CF_PARAMS=${CF_PARAMS-""}

echo $CF_API;
echo $CF_DOMAIN;
echo $CF_USER;
echo "**********";
echo $CF_ORG;
echo $CF_SPACE;
echo $CF_APPNAME;
echo $CF_PARAMS;

##
# login to cloudfoundry
##
echo "Your Cloudfoundry Version"
echo "Currently disabled because of bug in version"
# cf --version

echo "logging in... cf login --skip-ssl-validation -a $CF_API -u $CF_USER -p $CF_PASSWORD -o $CF_ORG -s $CF_SPACE"
cf login --skip-ssl-validation -a $CF_API -u $CF_USER -p $CF_PASSWORD -s $CF_SPACE -o $CF_ORG 
if [ $? -ne 0 ]; then
	echo "Ohje, credentials are not valid, something wrong exit..."
	exit 1
fi
echo -e "\e[1;36m"
echo "Logged in!"
echo -e "\e[0m"

echo "Following spaces are also available"
cf spaces

echo "Pushing new app"
cf push $CF_APPNAME $CF_PARAMS