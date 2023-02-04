Imports System.IO
Imports System.Text.Json
Imports TiledLib
Imports TiledLib.Layer
Imports TiledLib.Objects
Imports TOWD.Business

Module Program
    Sub Main(args As String())
        Dim world As IWorld = New World()
        ProcessFile("E:\GIT\towd\Maps\Home.tmx", "Home.json", world)
        ProcessFile("E:\GIT\towd\Maps\Loft.tmx", "Loft.json", world)
        ProcessFile("E:\GIT\towd\Maps\Quotidian.tmx", "Quotidian.json", world)
        ProcessFile("E:\GIT\towd\Maps\World.tmx", "World.json", world)
        ProcessFile("E:\GIT\towd\Maps\Garrison.tmx", "Garrison.json", world)
    End Sub
    Const NameText = "Name"
    Private Sub ProcessFile(inputFilename As String, outputFilename As String, world As IWorld)
        Using stream = File.OpenRead(inputFilename)
            Dim tileTable As New Dictionary(Of Integer, (ITileset, Integer))
            Dim fromMap = TiledLib.Map.FromStream(stream, Function(ts) File.OpenRead(Path.Combine(Path.GetDirectoryName(inputFilename), ts.Source)))
            Dim map = world.CreateMap(fromMap.Properties(NameText))
            InitializeMap(map, fromMap)
            tileTable = InitializeTileTable(tileTable, fromMap)
            ProcessLayers(map, tileTable, fromMap)
            File.WriteAllText(outputFilename, JsonSerializer.Serialize(map.Data)) 'TODO: this can be IMap's "save" method
        End Using
    End Sub

    Private Function InitializeTileTable(tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As TiledLib.Map) As Dictionary(Of Integer, (ITileset, Integer))
        For Each tileSet In fromMap.Tilesets
            For tileIndex = 0 To tileSet.TileCount - 1
                tileTable(tileSet.FirstGid + tileIndex) = (tileSet, tileIndex)
            Next
        Next
        Return tileTable
    End Function

    Private Sub InitializeMap(map As IMap, fromMap As TiledLib.Map)
        map.SetSize(fromMap.Width, fromMap.Height)
    End Sub

    Private Sub ProcessLayers(map As IMap, tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As TiledLib.Map)
        For Each l In fromMap.Layers
            ProcessLayer(map, tileTable, fromMap, l)
        Next
    End Sub

    Private Sub ProcessLayer(map As IMap, tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As TiledLib.Map, l As BaseLayer)
        Select Case l.LayerType
            Case LayerType.tilelayer
                ProcessTileLayer(DirectCast(l, TileLayer), tileTable, map)
            Case LayerType.objectgroup
                ProcessObjectGroup(DirectCast(l, ObjectLayer), tileTable, fromMap.CellWidth, fromMap.CellHeight, map)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Const EventTypeText = "EventType"
    Const MessageText = "Message"
    Const TeleportText = "Teleport"
    Const CheckFlagText = "CheckFlag"
    Const SetFlagText = "SetFlag"
    Const GiveItemText = "GiveItem"
    Const TriggerText = "Trigger"
    Const PCText = "PC"
    Const NPCText = "NPC"
    Const GiveMoneyText = "GiveMoney"

    Private Sub ProcessObjectGroup(l As ObjectLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), cellWidth As Integer, cellHeight As Integer, map As IMap)
        Dim objectTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) DirectCast(x, TileObject))
        Dim eventTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) New EventData)
        For Each entry In objectTable
            Dim eventId = entry.Key
            Dim obj = entry.Value
            Dim tile = tileTable(obj.Gid)
            Dim properties As Dictionary(Of String, String) = GetObjectProperties(obj, tile)
            Select Case properties(EventTypeText)
                Case MessageText
                    eventTable = ProcessMessage(eventTable, eventId, properties)
                Case TeleportText
                    eventTable = ProcessTeleport(eventTable, eventId, properties)
                Case CheckFlagText
                    eventTable = ProcessCheckFlag(eventTable, eventId, properties)
                Case SetFlagText
                    eventTable = ProcessSetFlag(eventTable, eventId, properties)
                Case GiveItemText
                    eventTable = ProcessGiveItem(eventTable, eventId, properties)
                Case TriggerText
                    ProcessTrigger(cellWidth, cellHeight, map, eventTable, eventId, obj, properties)
                Case PCText
                    ProcessPC(cellWidth, cellHeight, map, obj, properties)
                Case GiveMoneyText
                    eventTable = ProcessGiveMoney(eventTable, eventId, properties)
                Case NPCText
                    ProcessNPC(cellWidth, cellHeight, map, eventTable, eventId, obj, properties)
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
    End Sub

    Const NullObject = "0"
    Const OnInteractText = "OnInteract"
    Const CreatureTypeText = "CreatureType"

    Private Sub ProcessNPC(cellWidth As Integer, cellHeight As Integer, map As IMap, eventTable As Dictionary(Of Integer, EventData), eventId As Integer, obj As TileObject, properties As Dictionary(Of String, String))
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        Dim eventData As EventData = Nothing
        If properties(OnInteractText) <> NullObject Then
            eventData = eventTable(CInt(properties(OnInteractText)))
        End If
        map.Data.Cells(row * map.Data.Columns + column).Creature = New CreatureData With
        {
            .CreatureType = CType(properties(CreatureTypeText), CreatureType),
            .OnInteract = eventData
        }
    End Sub

    Private Sub ProcessPC(cellWidth As Integer, cellHeight As Integer, map As IMap, obj As TileObject, properties As Dictionary(Of String, String))
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        map.Data.Cells(row * map.Data.Columns + column).Creature = New CreatureData With
        {
            .CreatureType = CType(properties(CreatureTypeText), CreatureType)
        }
    End Sub

    Const NextEventText = "NextEvent"
    Const AmountText = "Amount"

    Private ReadOnly Property ProcessGiveMoney(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        Get
            eventTable(eventId).EventType = EventType.GiveMoney
            eventTable(eventId).Integers(EventInteger.Amount) = CInt(properties(AmountText))
            AssignLink(eventTable, eventId, properties, NextEventText, LinkType.NextEvent)
            Return eventTable
        End Get
    End Property
    Const OnBumpText = "OnBump"
    Const OnEnterText = "OnEnter"

    Private Sub ProcessTrigger(cellWidth As Integer, cellHeight As Integer, ByRef map As IMap, ByRef eventTable As Dictionary(Of Integer, EventData), eventId As Integer, obj As TileObject, properties As Dictionary(Of String, String))
        eventTable(eventId).EventType = EventType.Trigger
        AssignLink(eventTable, eventId, properties, OnBumpText, LinkType.OnBump)
        AssignLink(eventTable, eventId, properties, OnEnterText, LinkType.OnEnter)
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        map.Data.Cells(row * map.Data.Columns + column).Trigger = eventTable(eventId)
    End Sub

    Private Sub AssignLink(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String), propertyName As String, linkType As LinkType)
        If properties(propertyName) <> NullObject Then
            eventTable(eventId).Links(linkType) = eventTable(CInt(properties(propertyName)))
        End If
    End Sub

    Const ItemTypeText = "ItemType"
    Const ItemCountText = "ItemCount"
    Private Function ProcessGiveItem(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.GiveItem
        eventTable(eventId).Strings(EventString.ItemType) = properties(ItemTypeText)
        eventTable(eventId).Integers(EventInteger.ItemCount) = CInt(properties(ItemCountText))
        AssignLink(eventTable, eventId, properties, NextEventText, LinkType.NextEvent)
        Return eventTable
    End Function

    Private Function ProcessSetFlag(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.SetFlag
        eventTable(eventId).Strings(EventString.FlagType) = properties(FlagTypeText)
        AssignLink(eventTable, eventId, properties, NextEventText, LinkType.NextEvent)
        Return eventTable
    End Function

    Const FlagTypeText = "FlagType"
    Const WhenClearText = "WhenClear"
    Const WhenSetText = "WhenSet"

    Private Function ProcessCheckFlag(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.CheckFlag
        eventTable(eventId).Strings(EventString.FlagType) = properties(FlagTypeText)
        AssignLink(eventTable, eventId, properties, WhenClearText, LinkType.WhenClear)
        AssignLink(eventTable, eventId, properties, WhenSetText, LinkType.WhenSet)
        Return eventTable
    End Function

    Const ToXText = "ToX"
    Const ToYText = "ToY"
    Const ToMapText = "ToMap"

    Private Function ProcessTeleport(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.Teleport
        eventTable(eventId).Integers(EventInteger.ToX) = CInt(properties(ToXText))
        eventTable(eventId).Integers(EventInteger.ToY) = CInt(properties(ToYText))
        eventTable(eventId).Strings(EventString.ToMap) = properties(ToMapText)
        AssignLink(eventTable, eventId, properties, NextEventText, LinkType.NextEvent)
        Return eventTable
    End Function

    Private Function ProcessMessage(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.Message
        eventTable(eventId).Strings(EventString.Message) = properties(MessageText)
        AssignLink(eventTable, eventId, properties, NextEventText, LinkType.NextEvent)
        Return eventTable
    End Function

    Private Function GetObjectProperties(obj As TileObject, tile As (ITileset, Integer)) As Dictionary(Of String, String)
        Dim properties = tile.Item1.TileProperties(tile.Item2).ToDictionary(Function(x) x.Key, Function(x) x.Value)
        For Each prop In obj.Properties
            properties(prop.Key) = prop.Value
        Next
        Return properties
    End Function

    Private Sub ProcessTileLayer(l As TileLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), map As IMap)
        Dim index = 0
        For row = 0 To l.Height - 1
            For column = 0 To l.Width - 1
                index = ProcessTile(l, tileTable, map, index)
            Next
        Next
    End Sub

    Const TerrainTypeText = "TerrainType"

    Private Function ProcessTile(l As TileLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), map As IMap, index As Integer) As Integer
        Dim cell = l.Data(index)
        Dim tile = tileTable(cell)
        Dim tileProperties = tile.Item1.TileProperties(tile.Item2)
        map.Data.Cells.Add(New MapCellData With
                       {
                        .TerrainType = CType(tileProperties(TerrainTypeText), TerrainType)
                       })
        index += 1
        Return index
    End Function
End Module
