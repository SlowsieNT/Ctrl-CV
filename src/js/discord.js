// Send friend request by UID (Discord)
function Discord_SendRequestByUID(aUID, aLoginToken) {
    fetch("https://discord.com/api/v9/users/@me/relationships/" + aUID, {
        "method": "PUT",
        "headers": {
            "authorization": aLoginToken,
            "content-type": "application/json"
        }, "body": "{}"
    });
}
// Cancel friend request by UID (Discord)
function Discord_CancelRequestByUID(aUID, aLoginToken) {
    fetch("https://discord.com/api/v9/users/@me/relationships/" + aUID, {
        "method": "DELETE",
        "headers": {
            "authorization": aLoginToken
        }, "body": null
    });
}
// Remove friend by UID (Discord)
function Discord_RemoveFriendByUID(aUID, aLoginToken) {
    fetch("https://discord.com/api/v9/users/@me/relationships/" + aUID, {
        "method": "DELETE", "headers": {
            "authorization": aLoginToken
        }, "body": null
    });
}
