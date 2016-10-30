import reqwest from "reqwest";

module.exports = {
    updateApplications : function(){
        return reqwest({
            url:"/api/applications",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    }
};