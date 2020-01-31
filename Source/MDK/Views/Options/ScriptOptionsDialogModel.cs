﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Malware.MDKServices;

namespace MDK.Views.Options
{
    /// <summary>
    /// The view model for <see cref="ScriptOptionsDialog"/>
    /// </summary>
    public class ScriptOptionsDialogModel : DialogViewModel
    {
        MDKProjectProperties _activeProject;

        /// <summary>
        /// Creates a new instance of <see cref="ScriptOptionsDialogModel"/>
        /// </summary>
        /// <param name="package"></param>
        /// <param name="projectProperties"></param>
        public ScriptOptionsDialogModel([NotNull] MDKPackage package, [NotNull] MDKProjectProperties projectProperties)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));
            ActiveProject = projectProperties ?? throw new ArgumentNullException(nameof(projectProperties));
        }

        /// <summary>
        /// The currently selected project
        /// </summary>
        public MDKProjectProperties ActiveProject
        {
            get => _activeProject;
            set
            {
                if (Equals(value, _activeProject))
                    return;
                _activeProject = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Saves any changed options
        /// </summary>
        /// <returns></returns>
        protected override bool OnSave()
        {
            if (ActiveProject.HasChanges)
                ActiveProject.Save();
            return true;
        }
    }
    /*
    public static class EnumHelper
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return (attributes.First() as DescriptionAttribute).Description;

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

        public static IEnumerable<KeyValuePair<T, string>> GetAllValuesAndDescriptions<T>() where T:System.Enum
        {
            Type t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException($"{nameof(t)} must be an enum type");

            return Enum.GetValues(t).Cast<Enum>().Select((e) => new KeyValuePair<T, string>(e, e.Description())).ToList();
        }
    }
    */
}
