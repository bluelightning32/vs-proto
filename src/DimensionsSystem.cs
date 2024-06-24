using System;
using System.Collections.Generic;

using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace Dimensions;

public class DimensionsSystem : ModSystem {
  private ICoreAPI _api;
  private bool _freeMiniDimensionIndexesDirty = false;
  private List<int> _freeMiniDimensionIndexes = new();
  public override void Start(ICoreAPI api) {
    _api = api;
  }

  public override void StartClientSide(ICoreClientAPI api) { }

  public override void StartServerSide(ICoreServerAPI api) {
    api.Event.SaveGameLoaded += OnSaveGameLoaded;
    api.Event.GameWorldSave += OnGameWorldSave;
  }

  private void OnGameWorldSave() {
    _freeMiniDimensionIndexes = ((ICoreServerAPI)_api).WorldManager.SaveGame.GetData("dimensions.freeDimensions", new());
  }

  private void OnSaveGameLoaded() {
    throw new NotImplementedException();
  }

  public override void Dispose() {
    base.Dispose();
  }
}
