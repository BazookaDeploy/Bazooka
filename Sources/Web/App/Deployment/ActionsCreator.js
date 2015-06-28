import Dispatcher from '../Base/Dispatcher';
import { ActionTypes } from './Constants';
import reqwest from "reqwest";

module.exports = {
  updateDeployment : function(id){
    reqwest({
      url:"/api/deployment/"+id,
      type:'json',
      contentType: 'application/json',
      method:"get"
    }).then((x => {
      Dispatcher.handleServerAction({
        type: ActionTypes.UPDATE_DEPLOYMENT,
        apps: x
      });
    }))
  }
};
