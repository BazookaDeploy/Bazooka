import { createStore } from 'redux'
import Actions from "./Actions";


var reducer = function(state= {}, action){
    switch(action.type){
        case Actions.AddApplications:
            return Object.assign({},state, {applications:action.applications})
        case Actions.AddUsers:
            return Object.assign({},state, {users:action.users})
        case Actions.AddGroups:
            return Object.assign({},state, {groups:action.groups})
        case Actions.AddEnviroments:
            return Object.assign({},state, {enviroments:action.enviroments})

        default:
            return state;
    }
}

var store = createStore(reducer, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());

export default store;