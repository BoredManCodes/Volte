using System.Linq;
using System.Threading.Tasks;
using Gommon;
using Qmmands;
using Volte.Commands.Results;
using Volte.Core.Models.Guild;

namespace Volte.Commands.Modules
{
    public sealed partial class UtilityModule : VolteModule
    {
        [Command("Tag")]
        [Priority(0)]
        [Description("Gets a tag's contents if it exists.")]
        [Remarks("Usage: |prefix|tag {name}")]
        public Task<ActionResult> TagAsync([Remainder] Tag tag)
        {
            tag.Uses += 1;
            Db.UpdateData(Context.GuildData);

            return Ok(tag.FormatContent(Context), async _ =>
            {
                if (Context.GuildData.Configuration.DeleteMessageOnTagCommandInvocation)
                {
                    try
                    {
                        await Context.Message.DeleteAsync();
                    }
                    catch
                    { }
                }
            }, false);
        }

        [Command("TagStats")]
        [Priority(1)]
        [Description("Shows stats for a tag.")]
        [Remarks("Usage: |prefix|tagstats {name}")]
        public async Task<ActionResult> TagStatsAsync([Remainder] Tag tag)
        {
            var u = await Context.Client.GetShardFor(Context.Guild).Rest.GetUserAsync(tag.CreatorId);

            return Ok(Context.CreateEmbedBuilder()
                .WithTitle($"Tag {tag.Name}")
                .AddField("Response", $"`{tag.Response}`", true)
                .AddField("Creator", $"{u}", true)
                .AddField("Uses", $"**{tag.Uses}**", true));
        }

        [Command("Tags")]
        [Description("Lists all available tags in the current guild.")]
        [Remarks("Usage: |prefix|tags")]
        public Task<ActionResult> TagsAsync()
            => Ok(Context.CreateEmbedBuilder(
                Context.GuildData.Extras.Tags.Count == 0
                    ? "None"
                    : $"`{Context.GuildData.Extras.Tags.Select(x => x.Name).Join("`, `")}`"
            ).WithTitle($"Available Tags for {Context.Guild.Name}"));
    }
}