﻿using System;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;

namespace JonathanRobbins.FollowTarget.LinkTypes
{
    public class FollowDroptree : Command
    {
        public override void Execute(CommandContext context)
        {
            string target = WebUtil.GetFormValue(context.Parameters["fieldId"]);

            Item contextItem = context.Items.FirstOrDefault();

            if (contextItem != null)
            {
                Guid guid;
                bool parsed = Guid.TryParse(target, out guid);

                Item targetItem = contextItem.Database.GetItem(parsed ? guid.ToString() : StringUtil.EnsurePrefix('/', target));

                target = targetItem != null ? targetItem.ID.ToString() : target;
            }

            Sitecore.Context.ClientPage.SendMessage(this, "item:load(id=" + target + ")");
        }
    }
}
