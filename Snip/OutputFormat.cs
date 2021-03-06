﻿#region File Information
/*
 * Copyright (C) 2012-2014 David Rudie
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02111, USA.
 */
#endregion

namespace Winter
{
    using System;
    using System.Globalization;
    using System.Resources;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Win32;

    public partial class OutputFormat : Form
    {
        private readonly string assemblyTitle = AssemblyInformation.AssemblyTitle;
        private readonly string assemblyAuthor = AssemblyInformation.AssemblyAuthor;
        private readonly string assemblyVersion = AssemblyInformation.AssemblyVersion;

        private string trackFormat;
        private string separatorFormat;
        private string artistFormat;
        private string albumFormat;

        private ResourceManager resourceManager = ResourceManager.CreateFileBasedResourceManager("Strings", Application.StartupPath + @"/Resources", null);

        public OutputFormat()
        {
            this.InitializeComponent();

            this.LoadSettings();

            try
            {
                this.textBoxTrackFormat.Text = this.trackFormat;
                this.textBoxSeparatorFormat.Text = this.separatorFormat;
                this.textBoxArtistFormat.Text = this.artistFormat;
                this.textBoxAlbumFormat.Text = this.albumFormat;
            }
            catch
            {
                this.SetDefaults();
                throw;
            }
        }

        private void ButtonDefaults_Click(object sender, EventArgs e)
        {
            this.SetDefaults();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.SaveSettings();

            this.Close();
        }

        private void LoadSettings()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "SOFTWARE\\{0}\\{1}\\{2}",
                    this.assemblyAuthor,
                    this.assemblyTitle,
                    this.assemblyVersion));

            if (registryKey != null)
            {
                this.trackFormat = Convert.ToString(registryKey.GetValue("Track Format", this.resourceManager.GetString("TrackFormat")), CultureInfo.CurrentCulture);

                this.separatorFormat = Convert.ToString(registryKey.GetValue("Separator Format", this.resourceManager.GetString("SeparatorFormat")), CultureInfo.CurrentCulture);

                this.artistFormat = Convert.ToString(registryKey.GetValue("Artist Format", this.resourceManager.GetString("ArtistFormat")), CultureInfo.CurrentCulture);

                this.albumFormat = Convert.ToString(registryKey.GetValue("Album Format", this.resourceManager.GetString("AlbumFormat")), CultureInfo.CurrentCulture);

                registryKey.Close();
            }
        }

        private void SaveSettings()
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "SOFTWARE\\{0}\\{1}\\{2}",
                    this.assemblyAuthor,
                    this.assemblyTitle,
                    this.assemblyVersion));

            registryKey.SetValue("Track Format", this.textBoxTrackFormat.Text, RegistryValueKind.String);

            registryKey.SetValue("Separator Format", this.textBoxSeparatorFormat.Text, RegistryValueKind.String);

            registryKey.SetValue("Artist Format", this.textBoxArtistFormat.Text, RegistryValueKind.String);

            registryKey.SetValue("Album Format", this.textBoxAlbumFormat.Text, RegistryValueKind.String);

            registryKey.Close();
        }

        private void SetDefaults()
        {
            this.trackFormat = this.resourceManager.GetString("TrackFormat");
            this.textBoxTrackFormat.Text = this.trackFormat;

            this.separatorFormat = this.resourceManager.GetString("SeparatorFormat");
            this.textBoxSeparatorFormat.Text = this.separatorFormat;

            this.artistFormat = this.resourceManager.GetString("ArtistFormat");
            this.textBoxArtistFormat.Text = this.artistFormat;

            this.albumFormat = this.resourceManager.GetString("AlbumFormat");
            this.textBoxAlbumFormat.Text = this.albumFormat;
        }
    }
}
