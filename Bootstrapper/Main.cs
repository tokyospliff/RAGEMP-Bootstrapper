using System;
using GTANetworkAPI;

namespace HighLifeBootstrapper
{
    /// <summary>
    /// HighLife V2 Bootstrapper - Fixed version 1.2.0.0 made by Classic#1337
    /// Resolves NullReferenceException crashes in colshape event handlers
    /// </summary>
    public class Main : Script
    {
        private static bool _debugMode = true;

        /// <summary>
        /// Called when the resource starts
        /// </summary>
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            try
            {
                NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] v1.2.0.0 Started");
                NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] Colshape null-check protection enabled");
                NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] Debug mode: " + (_debugMode ? "ON" : "OFF"));
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] ERROR in ResourceStart: {ex.Message}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Stack: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Called when a player enters a colshape
        /// CRITICAL: This method had NullReferenceException in original Bootstrapper v1.1.0.0
        /// </summary>
        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape colshape, Player player)
        {
            try
            {
                // CRITICAL NULL CHECKS - Prevents crash from original Bootstrapper
                if (player == null)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] WARNING: PlayerEnterColshape called with NULL player");
                    return;
                }

                if (colshape == null)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: PlayerEnterColshape called with NULL colshape for player {player.Name}");
                    return;
                }

                // Additional safety check - verify player still exists
                if (!player.Exists)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: Player {player.Name} no longer exists in PlayerEnterColshape");
                    return;
                }

                // Additional safety check - verify colshape still exists
                if (!colshape.Exists)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: Colshape no longer exists for player {player.Name}");
                    return;
                }

                if (_debugMode)
                {
                    NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Player {player.Name} entered colshape");
                }

                // JavaScript handlers will process the actual logic
                // This C# handler just ensures no null reference crashes occur
            }
            catch (Exception ex)
            {
                // Catch ANY exception to prevent server crash
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] ERROR in PlayerEnterColshape: {ex.Message}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Player: {(player != null ? player.Name : "NULL")}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Colshape: {(colshape != null ? "EXISTS" : "NULL")}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Stack: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Called when a player exits a colshape
        /// CRITICAL: Added null checks to prevent similar crashes
        /// </summary>
        [ServerEvent(Event.PlayerExitColshape)]
        public void OnPlayerExitColshape(ColShape colshape, Player player)
        {
            try
            {
                // CRITICAL NULL CHECKS
                if (player == null)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] WARNING: PlayerExitColshape called with NULL player");
                    return;
                }

                if (colshape == null)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: PlayerExitColshape called with NULL colshape for player {player.Name}");
                    return;
                }

                // Additional safety check - verify player still exists
                if (!player.Exists)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: Player {player.Name} no longer exists in PlayerExitColshape");
                    return;
                }

                // Additional safety check - verify colshape still exists
                if (!colshape.Exists)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] WARNING: Colshape no longer exists for player {player.Name}");
                    return;
                }

                if (_debugMode)
                {
                    NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Player {player.Name} exited colshape");
                }

                // JavaScript handlers will process the actual logic
            }
            catch (Exception ex)
            {
                // Catch ANY exception to prevent server crash
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] ERROR in PlayerExitColshape: {ex.Message}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Player: {(player != null ? player.Name : "NULL")}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Colshape: {(colshape != null ? "EXISTS" : "NULL")}");
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Stack: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Called when a player disconnects
        /// Added for cleanup purposes
        /// </summary>
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            try
            {
                if (player == null)
                {
                    if (_debugMode)
                        NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] WARNING: PlayerDisconnected called with NULL player");
                    return;
                }

                if (_debugMode)
                {
                    NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] Player {player.Name} disconnected ({type}: {reason})");
                }
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] ERROR in PlayerDisconnected: {ex.Message}");
            }
        }

        /// <summary>
        /// Called when resource stops
        /// </summary>
        [ServerEvent(Event.ResourceStop)]
        public void OnResourceStop()
        {
            try
            {
                NAPI.Util.ConsoleOutput("[HighLifeBootstrapper] Shutting down...");
            }
            catch (Exception ex)
            {
                NAPI.Util.ConsoleOutput($"[HighLifeBootstrapper] ERROR in ResourceStop: {ex.Message}");
            }
        }
    }
}
