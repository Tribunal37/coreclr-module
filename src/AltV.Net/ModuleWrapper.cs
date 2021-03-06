using System;
using System.Runtime.CompilerServices;
using AltV.Net.Elements.Entities;
using AltV.Net.Native;

[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("AltV.Net.Mock")]

namespace AltV.Net
{
    internal static class ModuleWrapper
    {
        private static Module _module;

        private static IResource _resource;

        public static void Main(IntPtr serverPointer, IntPtr resourcePointer, string resourceName, string entryPoint)
        {
            _resource = new ResourceLoader(serverPointer, resourceName, entryPoint).Init();
            if (_resource == null)
            {
                return;
            }

            var playerFactory = _resource.GetPlayerFactory();
            var vehicleFactory = _resource.GetVehicleFactory();
            var blipFactory = _resource.GetBlipFactory();
            var checkpointFactory = _resource.GetCheckpointFactory();
            var voiceChannelFactory = _resource.GetVoiceChannelFactory();
            var playerPool = _resource.GetPlayerPool(playerFactory);
            var vehiclePool = _resource.GetVehiclePool(vehicleFactory);
            var blipPool = _resource.GetBlipPool(blipFactory);
            var checkpointPool = _resource.GetCheckpointPool(checkpointFactory);
            var voiceChannelPool = _resource.GetVoiceChannelPool(voiceChannelFactory);
            var entityPool = _resource.GetBaseEntityPool(playerPool, vehiclePool);
            var baseObjectPool = _resource.GetBaseBaseObjectPool(playerPool, vehiclePool, blipPool, checkpointPool, voiceChannelPool);
            var server = new Server(serverPointer, baseObjectPool, entityPool, playerPool, vehiclePool, blipPool,
                checkpointPool, voiceChannelPool);
            var csharpResource = new CSharpNativeResource(resourcePointer);
            _module = _resource.GetModule(server, csharpResource, baseObjectPool, entityPool, playerPool, vehiclePool,
                blipPool, checkpointPool, voiceChannelPool);
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            _resource.OnStart();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Alt.Log(
                $"< ==== UNHANDLED EXCEPTION ==== > {Environment.NewLine} Received an unhandled exception from {sender?.GetType()}: " +
                (Exception) e.ExceptionObject);
        }

        public static void OnStop()
        {
            _resource.OnStop();
        }

        public static void OnTick()
        {
            _resource.OnTick();
        }

        public static void OnCheckpoint(IntPtr checkpointPointer, IntPtr entityPointer, BaseObjectType baseObjectType,
            bool state)
        {
            _module.OnCheckpoint(checkpointPointer, entityPointer, baseObjectType, state);
        }

        public static void OnPlayerConnect(IntPtr playerPointer, ushort playerId, string reason)
        {
            _module.OnPlayerConnect(playerPointer, playerId, reason);
        }

        public static void OnPlayerDamage(IntPtr playerPointer, IntPtr attackerEntityPointer,
            BaseObjectType attackerBaseObjectType,
            ushort attackerEntityId, uint weapon, ushort damage)
        {
            _module.OnPlayerDamage(playerPointer, attackerEntityPointer, attackerBaseObjectType, attackerEntityId,
                weapon, damage);
        }

        public static void OnPlayerDeath(IntPtr playerPointer, IntPtr killerEntityPointer,
            BaseObjectType killerBaseObjectType, uint weapon)
        {
            _module.OnPlayerDeath(playerPointer, killerEntityPointer, killerBaseObjectType, weapon);
        }

        public static void OnPlayerChangeVehicleSeat(IntPtr vehiclePointer, IntPtr playerPointer, byte oldSeat,
            byte newSeat)
        {
            _module.OnPlayerChangeVehicleSeat(vehiclePointer, playerPointer, oldSeat, newSeat);
        }

        public static void OnPlayerEnterVehicle(IntPtr vehiclePointer, IntPtr playerPointer, byte seat)
        {
            _module.OnPlayerEnterVehicle(vehiclePointer, playerPointer, seat);
        }

        public static void OnPlayerLeaveVehicle(IntPtr vehiclePointer, IntPtr playerPointer, byte seat)
        {
            _module.OnPlayerLeaveVehicle(vehiclePointer, playerPointer, seat);
        }

        public static void OnPlayerDisconnect(IntPtr playerPointer, string reason)
        {
            _module.OnPlayerDisconnect(playerPointer, reason);
        }

        public static void OnClientEvent(IntPtr playerPointer, string name, ref MValueArray args)
        {
            _module.OnClientEvent(playerPointer, name, ref args);
        }

        public static void OnServerEvent(string name, ref MValueArray args)
        {
            _module.OnServerEvent(name, ref args);
        }

        public static void OnCreatePlayer(IntPtr playerPointer, ushort playerId)
        {
            _module.OnCreatePlayer(playerPointer, playerId);
        }

        public static void OnRemovePlayer(IntPtr playerPointer)
        {
            _module.OnRemovePlayer(playerPointer);
        }

        public static void OnCreateVehicle(IntPtr vehiclePointer, ushort vehicleId)
        {
            _module.OnCreateVehicle(vehiclePointer, vehicleId);
        }

        public static void OnRemoveVehicle(IntPtr vehiclePointer)
        {
            _module.OnRemoveVehicle(vehiclePointer);
        }

        public static void OnCreateBlip(IntPtr blipPointer)
        {
            _module.OnCreateBlip(blipPointer);
        }
        
        public static void OnCreateVoiceChannel(IntPtr channelPointer)
        {
            _module.OnCreateVoiceChannel(channelPointer);
        }

        public static void OnRemoveBlip(IntPtr blipPointer)
        {
            _module.OnRemoveBlip(blipPointer);
        }

        public static void OnCreateCheckpoint(IntPtr checkpointPointer)
        {
            _module.OnCreateCheckpoint(checkpointPointer);
        }

        public static void OnRemoveCheckpoint(IntPtr checkpointPointer)
        {
            _module.OnRemoveCheckpoint(checkpointPointer);
        }
        
        public static void OnRemoveVoiceChannel(IntPtr channelPointer)
        {
            _module.OnRemoveVoiceChannel(channelPointer);
        }

        public static void OnPlayerRemove(IntPtr playerPointer)
        {
            _module.OnPlayerRemove(playerPointer);
        }

        public static void OnVehicleRemove(IntPtr vehiclePointer)
        {
            _module.OnVehicleRemove(vehiclePointer);
        }

        public static void OnConsoleCommand(string name, StringViewArray args)
        {
            _module.OnConsoleCommand(name, args);
        }
    }
}