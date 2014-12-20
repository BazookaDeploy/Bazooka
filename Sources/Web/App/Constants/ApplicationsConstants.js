var keyMirror = require("keymirror");

module.exports = {
  ActionTypes : keyMirror({
    UPDATE_APPS: null
  }),
  PayloadSources : keyMirror({
    SERVER_ACTION: null,
    VIEW_ACTION: null
  })
}
