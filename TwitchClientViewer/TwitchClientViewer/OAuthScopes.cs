namespace TwitchClientViewer
{
    //"user_read"; //: Read access to non-public user information, such as email address.
    //"user_blocks_edit"; //: Ability to ignore or unignore on behalf of a user.
    //"user_blocks_read"; //: Read access to a user's list of ignored users.
    //"user_follows_edit"; //: Access to manage a user's followed channels.
    //"channel_read"; //: Read access to non-public channel information, including email address and stream key.
    //"channel_editor";//: Write access to channel metadata(game, status, etc).
    //"channel_commercial";//: Access to trigger commercials on channel.
    //"channel_stream"; //: Ability to reset a channel's stream key.
    //"channel_subscriptions"; //: Read access to all subscribers to your channel.
    //"user_subscriptions"; //: Read access to subscriptions of a user.
    //"channel_check_subscription";//: Read access to check if a user is subscribed to your channel.
    //"chat_login"; //: Ability to log into chat and send messages.
    enum OAuthScopes
    {
        user_read,
        user_follows_edit,
        channel_subscriptions,
        user_subscriptions,
        channel_read,
    }
}
