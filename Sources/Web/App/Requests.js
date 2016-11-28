import reqwest from "reqwest";

module.exports = {
    updateApplications : function(){
        return reqwest({
            url:"/api/applications",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    },

    updateUsers : function(){
        return reqwest({
            url:"/api/users/all",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    },

    updateGroups : function(){
        return reqwest({
            url:"/api/users/groups",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    },

    updateEnviroments : function(){
        return reqwest({
            url:"/api/enviroments",
            type:'json',
            contentType: 'application/json',
            method:"get"
        })
    },
};