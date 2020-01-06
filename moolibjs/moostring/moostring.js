//String prototypes

const stringInsert = function (s1, s2, index) {
	return (index > 0) ? s1.substring(0, index) + s2 + s1.substring(index, this.length) : s1 + s2;
};

const safeString = function (s1 = "") {
	return (typeof s1 === 'string' || s1 instanceof String) ? s1 : "";
};

// string.format prototype with variable args
// Inserts passed paramaters into string where there is space
// <string containing numbered paramaters such as {0} and {1}>.format("string1","string2");

String.prototype.format = function() {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function(match, number) { 
    return typeof args[number] != 'undefined'
        ? args[number]
        : match
    ;
    });
};

module.exports = {
	stringInsert,
	safeString
};