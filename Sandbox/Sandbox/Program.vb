Imports System.IO
Imports System.Text.Json
Imports TiledLib
Imports TiledLib.Layer
Imports TiledLib.Objects
Module Program
    Sub Main(args As String())
        ProcessFile("E:\GIT\towd\Maps\Home.tmx", "Home.json")
        ProcessFile("E:\GIT\towd\Maps\Loft.tmx", "Loft.json")
        ProcessFile("E:\GIT\towd\Maps\Quotidian.tmx", "Quotidian.json")
        ProcessFile("E:\GIT\towd\Maps\World.tmx", "World.json")
        ProcessFile("E:\GIT\towd\Maps\Garrison.tmx", "Garrison.json")
    End Sub

    Private Sub ProcessFile(inputFilename As String, outputFilename As String)
        Dim data As New MapData
        Using stream = File.OpenRead(inputFilename)
            Dim tileTable As New Dictionary(Of Integer, (ITileset, Integer))
            Dim fromMap = Map.FromStream(stream, Function(ts) File.OpenRead(Path.Combine(Path.GetDirectoryName(inputFilename), ts.Source)))
            data = InitializeMap(data, fromMap)
            tileTable = InitializeTileTable(tileTable, fromMap)
            ProcessLayers(data, tileTable, fromMap)
        End Using
        File.WriteAllText(outputFilename, JsonSerializer.Serialize(data))
    End Sub

    Private Function InitializeTileTable(tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As Map) As Dictionary(Of Integer, (ITileset, Integer))
        For Each tileSet In fromMap.Tilesets
            For tileIndex = 0 To tileSet.TileCount - 1
                tileTable(tileSet.FirstGid + tileIndex) = (tileSet, tileIndex)
            Next
        Next

        Return tileTable
    End Function

    Private Function InitializeMap(data As MapData, fromMap As Map) As MapData
        data.Columns = fromMap.Width
        data.Rows = fromMap.Height
        data.Cells.Clear()
        Return data
    End Function

    Private Sub ProcessLayers(data As MapData, tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As Map)
        For Each l In fromMap.Layers
            ProcessLayer(data, tileTable, fromMap, l)
        Next
    End Sub

    Private Sub ProcessLayer(data As MapData, tileTable As Dictionary(Of Integer, (ITileset, Integer)), fromMap As Map, l As BaseLayer)
        Select Case l.LayerType
            Case LayerType.tilelayer
                ProcessTileLayer(DirectCast(l, TileLayer), tileTable, data)
            Case LayerType.objectgroup
                ProcessObjectGroup(DirectCast(l, ObjectLayer), tileTable, fromMap.CellWidth, fromMap.CellHeight, data)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub

    Private Sub ProcessObjectGroup(l As ObjectLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), cellWidth As Integer, cellHeight As Integer, data As MapData)
        Dim objectTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) DirectCast(x, TileObject))
        Dim eventTable = l.Objects.ToDictionary(Function(x) x.Id, Function(x) New EventData)
        For Each entry In objectTable
            Dim eventId = entry.Key
            Dim obj = entry.Value
            Dim tile = tileTable(obj.Gid)
            Dim properties As Dictionary(Of String, String) = GetObjectProperties(obj, tile)
            Select Case properties("EventType")
                Case "Message"
                    eventTable = ProcessMessage(eventTable, eventId, properties)
                Case "Teleport"
                    eventTable = ProcessTeleport(eventTable, eventId, properties)
                Case "CheckFlag"
                    eventTable = ProcessCheckFlag(eventTable, eventId, properties)
                Case "SetFlag"
                    eventTable = ProcessSetFlag(eventTable, eventId, properties)
                Case "GiveItem"
                    eventTable = ProcessGiveItem(eventTable, eventId, properties)
                Case "Trigger"
                    ProcessTrigger(cellWidth, cellHeight, data, eventTable, eventId, obj, properties)
                Case "PC"
                    ProcessPC(cellWidth, cellHeight, data, obj, properties)
                Case "GiveMoney"
                    eventTable = ProcessGiveMoney(eventTable, eventId, properties)
                Case "NPC"
                    ProcessNPC(cellWidth, cellHeight, data, eventTable, eventId, obj, properties)
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
    End Sub

    Private Sub ProcessNPC(cellWidth As Integer, cellHeight As Integer, data As MapData, eventTable As Dictionary(Of Integer, EventData), eventId As Integer, obj As TileObject, properties As Dictionary(Of String, String))
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        Dim eventData As EventData = Nothing
        If properties("OnInteract") <> "0" Then
            eventData = eventTable(CInt(properties("OnInteract")))
        End If
        data.Cells(row * data.Columns + column).Creature = New CreatureData With
        {
            .CreatureType = CType(properties("CreatureType"), CreatureType),
            .OnInteract = eventData
        }
    End Sub

    Private Sub ProcessPC(cellWidth As Integer, cellHeight As Integer, data As MapData, obj As TileObject, properties As Dictionary(Of String, String))
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        data.Cells(row * data.Columns + column).Creature = New CreatureData With
        {
            .CreatureType = CType(properties("CreatureType"), CreatureType)
        }
    End Sub

    Private ReadOnly Property ProcessGiveMoney(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        Get
            eventTable(eventId).EventType = EventType.GiveMoney
            eventTable(eventId).Integers(EventInteger.Amount) = CInt(properties("Amount"))
            If properties("NextEvent") <> "0" Then
                eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
            End If
            Return eventTable
        End Get
    End Property

    Private Sub ProcessTrigger(cellWidth As Integer, cellHeight As Integer, ByRef data As MapData, ByRef eventTable As Dictionary(Of Integer, EventData), eventId As Integer, obj As TileObject, properties As Dictionary(Of String, String))
        eventTable(eventId).EventType = EventType.Trigger
        If properties("OnBump") <> "0" Then
            eventTable(eventId).Links(LinkType.OnBump) = eventTable(CInt(properties("OnBump")))
        End If
        If properties("OnEnter") <> "0" Then
            eventTable(eventId).Links(LinkType.OnEnter) = eventTable(CInt(properties("OnEnter")))
        End If
        Dim column = CInt(obj.X) \ cellWidth
        Dim row = CInt(obj.Y) \ cellHeight - 1
        data.Cells(row * data.Columns + column).Trigger = eventTable(eventId)
    End Sub

    Private Function ProcessGiveItem(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.GiveItem
        eventTable(eventId).Integers(EventInteger.ItemId) = CInt(properties("ItemId"))
        eventTable(eventId).Integers(EventInteger.ItemCount) = CInt(properties("ItemCount"))
        If properties("NextEvent") <> "0" Then
            eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
        End If
        Return eventTable
    End Function

    Private Function ProcessSetFlag(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.SetFlag
        eventTable(eventId).Strings(EventString.Flag) = properties("Flag")
        If properties("NextEvent") <> "0" Then
            eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
        End If

        Return eventTable
    End Function

    Private Function ProcessCheckFlag(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.CheckFlag
        eventTable(eventId).Strings(EventString.Flag) = properties("Flag")
        If properties("WhenClear") <> "0" Then
            eventTable(eventId).Links(LinkType.WhenClear) = eventTable(CInt(properties("WhenClear")))
        End If
        If properties("WhenSet") <> "0" Then
            eventTable(eventId).Links(LinkType.WhenSet) = eventTable(CInt(properties("WhenSet")))
        End If

        Return eventTable
    End Function

    Private Function ProcessTeleport(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.Teleport
        eventTable(eventId).Integers(EventInteger.ToX) = CInt(properties("ToX"))
        eventTable(eventId).Integers(EventInteger.ToY) = CInt(properties("ToY"))
        eventTable(eventId).Strings(EventString.ToMap) = properties("ToMap")
        If properties("NextEvent") <> "0" Then
            eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
        End If

        Return eventTable
    End Function

    Private Function ProcessMessage(eventTable As Dictionary(Of Integer, EventData), eventId As Integer, properties As Dictionary(Of String, String)) As Dictionary(Of Integer, EventData)
        eventTable(eventId).EventType = EventType.Message
        eventTable(eventId).Strings(EventString.Message) = properties("Message")
        If properties("NextEvent") <> "0" Then
            eventTable(eventId).Links(LinkType.NextEvent) = eventTable(CInt(properties("NextEvent")))
        End If

        Return eventTable
    End Function

    Private Function GetObjectProperties(obj As TileObject, tile As (ITileset, Integer)) As Dictionary(Of String, String)
        Dim properties = tile.Item1.TileProperties(tile.Item2).ToDictionary(Function(x) x.Key, Function(x) x.Value)
        For Each prop In obj.Properties
            properties(prop.Key) = prop.Value
        Next

        Return properties
    End Function

    Private Sub ProcessTileLayer(l As TileLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), data As MapData)
        Dim index = 0
        For row = 0 To l.Height - 1
            For column = 0 To l.Width - 1
                index = ProcessTile(l, tileTable, data, index)
            Next
        Next
    End Sub

    Private Function ProcessTile(l As TileLayer, tileTable As Dictionary(Of Integer, (ITileset, Integer)), data As MapData, index As Integer) As Integer
        Dim cell = l.Data(index)
        Dim tile = tileTable(cell)
        Dim tileProperties = tile.Item1.TileProperties(tile.Item2)
        data.Cells.Add(New MapCellData With
                       {
                        .TerrainType = CType(tileProperties("TerrainType"), TerrainType)
                       })
        index += 1
        Return index
    End Function
End Module
