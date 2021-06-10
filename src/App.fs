module App
open Fable.Core
open Fable.Core.JsInterop
JS.console.log "Hello from Fable!"

open FSharp.Data
open FSharp.Core
open System.IO
open Browser.Dom
open Fable.SimpleHttp
open Fable.SimpleJson
//open FSharp.Data.TypeProviders
//open Fable.JsonProvider

let firstDiv = document.createElement("div")
firstDiv.setAttribute("id", "firstDiv")
firstDiv.setAttribute("class","firstDivClass")

let spaceDiv = document.createElement("div")
spaceDiv.setAttribute("class","spaceDiv")

let btnDiv = document.createElement("div")
btnDiv.setAttribute("id","btnDiv")
btnDiv.setAttribute("class","btnclass")

let labelTxt = document.createTextNode(" Select Card ")
let labelTxt1 = document.createTextNode(" ")
let labelViewChange = document.createElement("label")
labelViewChange.appendChild(labelTxt) |> ignore
labelViewChange.appendChild(labelTxt1) |> ignore

let chkInput = document.createElement("input")
chkInput.setAttribute("id","viewChk")
chkInput.setAttribute("type","checkbox")
chkInput.setAttribute("data-toggle","toggle")

labelViewChange.appendChild(chkInput) |> ignore
btnDiv.appendChild(labelViewChange) |> ignore

let newtable = document.createElement("table")
newtable.setAttribute("id", "tableid")
newtable.setAttribute("border", "1")
newtable.setAttribute("class", "table")

let newCard = document.createElement("div")
let cardInnerDiv = document.createElement("div")
cardInnerDiv.setAttribute("class","forContentCls")
cardInnerDiv.appendChild(newCard) |> ignore
cardInnerDiv.setAttribute("style","display:none")

firstDiv.appendChild(btnDiv)|> ignore
firstDiv.appendChild(spaceDiv)|> ignore
firstDiv.appendChild(newtable) |> ignore
firstDiv.appendChild(cardInnerDiv) |> ignore
document.body.appendChild(firstDiv)  |> ignore

let table = document.getElementById("tableid");
let tr = document.createElement("tr")
let th1 = document.createElement("th")
let th2 = document.createElement("tth")
let th3 = document.createElement("th")
let th4 = document.createElement("th")
let th5 = document.createElement("th")
let th6 = document.createElement("th")

let date = document.createTextNode("Date")
let open1 = document.createTextNode("Open")
let high = document.createTextNode("High")
let low = document.createTextNode("Low")
let close = document.createTextNode("Close")
let volume = document.createTextNode("Volume")

th1.appendChild(date) |> ignore
th2.appendChild(open1) |> ignore
th3.appendChild(high) |> ignore
th4.appendChild(low) |> ignore
th5.appendChild(close) |> ignore
th6.appendChild(volume) |> ignore
tr.appendChild(th1) |> ignore
tr.appendChild(th2) |> ignore
tr.appendChild(th3) |> ignore
tr.appendChild(th4) |> ignore
tr.appendChild(th5) |> ignore
tr.appendChild(th6) |> ignore
table.appendChild(tr) |> ignore


type JasonData =
    { Date: string;
    Open: decimal;
    High: decimal;
    Low: decimal;
    Close: decimal;
    Volume: decimal;}

let createTable (data: JasonData[]) = 
    let body = document.createElement("tbody")
    data
    |> Array.iter(fun x -> 
        let tr = document.createElement("tr")
        let td1 = document.createElement("td")
        let td2 = document.createElement("td")
        let td3 = document.createElement("td")
        let td4 = document.createElement("td")
        let td5 = document.createElement("td")
        let td6 = document.createElement("td")   
        let date = document.createTextNode(x.Date.ToString())
        let high = document.createTextNode(x.High.ToString())
        let low = document.createTextNode(x.Low.ToString())
        let openn = document.createTextNode(x.Open.ToString())
        let close = document.createTextNode(x.Close.ToString())
        let volume = document.createTextNode(x.Volume.ToString())   
        td1.appendChild(date) |>ignore
        td2.appendChild(high) |>ignore
        td3.appendChild(low) |>ignore
        td4.appendChild(openn) |>ignore
        td5.appendChild(close) |>ignore
        td6.appendChild(volume) |>ignore       
        tr.appendChild(td1) |>ignore
        tr.appendChild(td2) |>ignore
        tr.appendChild(td3) |>ignore
        tr.appendChild(td4) |>ignore
        tr.appendChild(td5) |>ignore
        tr.appendChild(td6) |>ignore
        body.appendChild(tr)
        |> ignore
    )
    table.appendChild(body)

    
let createCard (data: JasonData[]) = 
    cardInnerDiv.setAttribute("style","display:block")
    let dataArr = data |> Array.splitInto 90
    dataArr
    |> Array.iter(fun rowdata -> 
        let cRow = document.createElement("div")
        cRow.setAttribute("class","row")
        rowdata
        |> Array.iter(fun x ->        
        let colDiv = document.createElement("div")
        colDiv.setAttribute("class","col-md-4")
        let cDiv = document.createElement("div")
        cDiv.setAttribute("class","card cardCls")
        let cDiv2 = document.createElement("div")
        cDiv2.setAttribute("class","card-body text-center")
        let d1 = document.createElement("p")
        d1.setAttribute("class","card-text")
        let d2 = document.createElement("p")
        d2.setAttribute("class","card-text")
        let d3 = document.createElement("p")
        d3.setAttribute("class","card-text")
        let d4 = document.createElement("p")
        d4.setAttribute("class","card-text")
        let d5 = document.createElement("p")
        d5.setAttribute("class","card-text")
        let d6 = document.createElement("p")
        d6.setAttribute("class","card-text")       
        let date = document.createTextNode("Date : " + x.Date.ToString())
        let openn = document.createTextNode("Open : " + x.High.ToString())
        let close = document.createTextNode("High : " + x.Low.ToString())
        let high = document.createTextNode("Low : " + x.Open.ToString())
        let low = document.createTextNode("Close : " + x.Close.ToString())
        let volume = document.createTextNode("Volume : " + x.Volume.ToString())
        d1.appendChild(date) |>ignore
        d2.appendChild(openn) |>ignore
        d3.appendChild(close) |>ignore
        d4.appendChild(high) |>ignore
        d5.appendChild(low) |>ignore
        d6.appendChild(volume) |>ignore       
        cDiv2.appendChild(d1) |>ignore
        cDiv2.appendChild(d2) |>ignore
        cDiv2.appendChild(d3) |>ignore
        cDiv2.appendChild(d4) |>ignore
        cDiv2.appendChild(d5) |>ignore
        cDiv2.appendChild(d6) |>ignore
        cDiv.appendChild(cDiv2) |>ignore
        colDiv.appendChild(cDiv) |>ignore
        cRow.appendChild(colDiv) |>ignore
        newCard.appendChild(cRow) |>ignore
        newCard.appendChild(document.createElement("br"))
        |> ignore
    ))

  

let [<Literal>] JSON_URL = "http://localhost:8080/sampledata.json" 

let newResult = 
        async {
            let! (_, res) = Fable.SimpleHttp.Http.get JSON_URL
            let simple = res |> Json.parseAs<JasonData[]>
            createTable simple |> ignore   
            createCard simple |> ignore         
        }  |> Async.StartImmediate


    
    
