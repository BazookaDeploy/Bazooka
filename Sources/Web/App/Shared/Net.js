import reqwest from "reqwest";

module.exports = {
    get: function (url) {
        var promise = reqwest({
            url: url,
            type: 'json',
            contentType: 'application/json',
            method: "get"
        });
        
        return promise;
    },

    post: function (url, data) {
        var promise = reqwest({
            url: url,
            type: 'json',
            contentType: 'application/json',
            method: "post",
            data: JSON.stringify(data)
        });

        return promise;
    }
};