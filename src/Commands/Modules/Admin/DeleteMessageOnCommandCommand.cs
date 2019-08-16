﻿using System.Threading.Tasks;
using Qmmands;
using Volte.Commands.Checks;
using Volte.Commands.Results;

namespace Volte.Commands.Modules
{
    public sealed partial class AdminModule : VolteModule
    {
        [Command("DeleteMessageOnCommand", "Dmoc")]
        [Description("Enable/Disable deleting the command message upon execution of a command for this guild.")]
        [Remarks("Usage: |prefix|deletemessageoncommand {true|false}")]
        [RequireGuildAdmin]
        public Task<ActionResult> DeleteMessageOnCommandAsync(bool enabled)
        {
            Context.GuildData.Configuration.DeleteMessageOnCommand = enabled;
            Db.UpdateData(Context.GuildData);
            return Ok(enabled
                ? "Enabled DeleteMessageOnCommand in this server."
                : "Disabled DeleteMessageOnCommand in this server.");
        }

        [Command("DeleteMessageOnTagCommand", "Dmotc")]
        [Description(
            "Enable/Disable deleting the command message upon usage of the tag retrieval command for this guild.")]
        [Remarks("Usage: |prefix|deletemessageontagcommand {true|false}")]
        public Task<ActionResult> DeleteMessageOnTagCommand(bool enabled)
        {
            Context.GuildData.Configuration.DeleteMessageOnTagCommandInvocation = enabled;
            Db.UpdateData(Context.GuildData);
            return Ok(enabled
                ? "Enabled DeleteMessageOnTagCommand in this server."
                : "Disabled DeleteMessageOnTagCommand in this server.");
        }
    }
}