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
    using Microsoft.Win32;

    public static class Settings
    {
        public static void Save()
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "SOFTWARE\\{0}\\{1}\\{2}",
                    AssemblyInformation.AssemblyAuthor,
                    AssemblyInformation.AssemblyTitle,
                    AssemblyInformation.AssemblyVersion));

            registryKey.SetValue("Player", (int)Globals.PlayerSelection);

            if (Globals.SaveSeparateFiles)
            {
                registryKey.SetValue("Save Separate Files", "true");
            }
            else
            {
                registryKey.SetValue("Save Separate Files", "false");
            }

            if (Globals.SaveAlbumArtwork)
            {
                registryKey.SetValue("Save Album Artwork", "true");
            }
            else
            {
                registryKey.SetValue("Save Album Artwork", "false");
            }

            if (Globals.KeepSpotifyAlbumArtwork)
            {
                registryKey.SetValue("Keep Spotify Album Artwork", "true");
            }
            else
            {
                registryKey.SetValue("Keep Spotify Album Artwork", "false");
            }

            registryKey.SetValue("Album Artwork Resolution", (int)Globals.ArtworkResolution);

            if (Globals.SaveHistory)
            {
                registryKey.SetValue("Save History", "true");
            }
            else
            {
                registryKey.SetValue("Save History", "false");
            }

            registryKey.Close();
        }

        public static void Load()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "SOFTWARE\\{0}\\{1}\\{2}",
                    AssemblyInformation.AssemblyAuthor,
                    AssemblyInformation.AssemblyTitle,
                    AssemblyInformation.AssemblyVersion));

            if (registryKey != null)
            {
                Globals.PlayerSelection = (Globals.MediaPlayerSelection)registryKey.GetValue("Player", Globals.MediaPlayerSelection.Spotify);

                bool saveSeparateFilesChecked = Convert.ToBoolean(registryKey.GetValue("Save Separate Files", false), CultureInfo.InvariantCulture);

                if (saveSeparateFilesChecked)
                {
                    Globals.SaveSeparateFiles = true;
                }
                else
                {
                    Globals.SaveSeparateFiles = false;
                }

                bool saveAlbumArtworkChecked = Convert.ToBoolean(registryKey.GetValue("Save Album Artwork", false), CultureInfo.InvariantCulture);

                if (saveAlbumArtworkChecked)
                {
                    Globals.SaveAlbumArtwork = true;
                }
                else
                {
                    Globals.SaveAlbumArtwork = false;
                }

                bool keepSpotifyAlbumArtwork = Convert.ToBoolean(registryKey.GetValue("Keep Spotify Album Artwork", false), CultureInfo.InvariantCulture);

                if (keepSpotifyAlbumArtwork)
                {
                    Globals.KeepSpotifyAlbumArtwork = true;
                }
                else
                {
                    Globals.KeepSpotifyAlbumArtwork = false;
                }

                Globals.ArtworkResolution = (Globals.AlbumArtworkResolution)registryKey.GetValue("Album Artwork Resolution", Globals.AlbumArtworkResolution.Tiny);

                bool saveHistoryChecked = Convert.ToBoolean(registryKey.GetValue("Save History", false), CultureInfo.InvariantCulture);

                if (saveHistoryChecked)
                {
                    Globals.SaveHistory = true;
                }
                else
                {
                    Globals.SaveHistory = false;
                }

                Globals.TrackFormat = Convert.ToString(registryKey.GetValue("Track Format", "“$t”"), CultureInfo.CurrentCulture);

                Globals.SeparatorFormat = Convert.ToString(registryKey.GetValue("Separator Format", " ― "), CultureInfo.CurrentCulture);

                Globals.ArtistFormat = Convert.ToString(registryKey.GetValue("Artist Format", "$a"), CultureInfo.CurrentCulture);

                Globals.AlbumFormat = Convert.ToString(registryKey.GetValue("Album Format", "$l"), CultureInfo.CurrentCulture);

                registryKey.Close();
            }
        }
    }
}
