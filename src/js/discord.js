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
// Fetch profile by UID (Discord)
function Discord_GetProfileByUID(aUID, aLoginToken, aCallback) {
	var vX = new XMLHttpRequest;
	vX.open("GET", "https://discord.com/api/v9/users/" + aUID + "/profile?with_mutual_guilds=true");
	vX.setRequestHeader("authorization", aLoginToken);
	vX.onreadystatechange = function () {
		if (4 == vX.readyState && aCallback)
			aCallback(JSON.parse(vX.responseText));
	};
	vX.send();
}
// Discord_GetProfileByUID("123", "token", function(aObj){console.log(aObj);});


// Deletions
function Discord_DeleteOwnAccount(aPassword, aLoginToken) {
	fetch("https://discord.com/api/v9/users/@me/delete", {
		"method": "POST", "headers": {
			"authorization": aLoginToken
		}, "body": JSON.stringify({password:aPassword}),
	});
}
function Discord_DeleteServer(aGID, aLoginToken) {
	fetch("https://discord.com/api/v9/guilds/"+aGID+"/delete", {
		"method": "POST", "headers": {
			"authorization": aLoginToken
		}, "body": "{}"
	});
}
