const axios = require('axios');
const fs = require('file-system');
const chalk = require('chalk');

let dataPath = "./";
let downloadsPending = 0;


// configure global moonet settings, such as the cachePath
// cachePath is the path of the cache, a slash will be appended if there isn't one
//      has security implications so hard code it, do not pass it from the commandline

const config = (cachePath) => {
    dataPath = cachePath.substr(-1,1) != "/" ? cachePath + "/" : cachePath;
}


// numDownloadsPending, the number of pending downloads
// ensure the thread sleeps if using this as a loop condition

const numDownloadsPending = () => {
    return downloadsPending;
}


// Download URL to file if it doesn't exist locally, or if the cache has expired
// filename has security implications - do not pass in directly from commandline or web,
//      and it should be cache-unique

const downloadFile = async (url, filename, cacheDuration) => {
    
    let response = _getLocalCache(filename, cacheDuration);

    // if there's nothing in the local cache, or it's expired, then download
    if (response == null) {
        response = await _downloadFile(url, filename)
        .catch((error) => {
            console.log(chalk.red(error));
        });
    }
};


const _getLocalCache = (filename, cacheDuration) => {

    //Does the file exist locally? 
    let localFile = dataPath + filename;
    if (fs.existsSync(localFile)) {

        // Has the cache expired?
        let stat = fs.statSync(localFile);
        if (stat.mtimeMs + cacheDuration > (new Date).getTime()) {
            let response = fs.readFileSync(localFile);
            return response; 
        } 
    }

    return null;
};


// _download file, internal asynchronous download of file and save to cache

const _downloadFile = async (url, filename) => {
    let response;

    try {
        console.log(`Fetching ${url}`);
        response = await axios.get(url, {
            withCredentials: true
        });
    } catch (e) {
        throw new Error(`${url} failed to download. ${e}`);
    }

    _writeCache(response.data, filename);
    return response;
};


// _write cache, internal write data to file in the cache

const _writeCache = (data, filename) => {

    // Make sure the cache path exists
    if (!fs.existsSync(dataPath)) {
        fs.mkdirSync(dataPath);
    }

    fs.writeFileSync(dataPath + filename, data);
};


module.exports = {
    config,
    downloadFile,
    numDownloadsPending,
};