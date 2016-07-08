"use strict";

var jsonfile = require("jsonfile");
var semver = require("semver");

var file = "project.json";

var updateVersion = function (err, project) {
    if (err) {
        console.error(err);
        return;
    }

    var buildNumber = process.env.APPVEYOR_BUILD_NUMBER;
    if (process.argv[2] != undefined) {
        buildNumber = process.argv[2];
    }

    if (buildNumber == undefined) {
        console.error("No build number");
        return;
    }

    var version = semver.valid(project.version.replace(/\-(alpha|beta|rc\d*)\-\*/i, "-$1-" + buildNumber));
    project.version = version;

    jsonfile.writeFile(file, project, { spaces: 2 }, onVersionUpdated);
};

var onVersionUpdated = function (err) {
    if (err) {
        console.error(err);
        return;
    }

    console.log("version in project.json has been updated");
};

jsonfile.readFile(file, updateVersion);
