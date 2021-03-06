import React from "react";
import Navbar from "./components/Navbar";
import { Switch, Route } from "react-router-dom";
import Inicio from "./pages/Inicio";
import Drogas from "./pages/Drogas";
import DrogasDetalles from "./pages/DrogasDetalles";
import Medicamentos from "./pages/Medicamentos";
import MedicamentosDetalles from "./pages/MedicamentosDetalles";
import Reposiciones from "./pages/Reposiciones";
import ReposicionesDetalles from "./pages/ReposicionesDetalles";
import Partidas from "./pages/Partidas";
import PartidasDetalles from "./pages/PartidasDetalles";
import Stock from "./pages/Stock";
import StockDetalles from "./pages/StockDetalles";

function App() {
  window.ApiUrl = "http://medicamentos.us-east-1.elasticbeanstalk.com/api/";

  return (
    <Navbar>
      <Switch>
        <Route exact path="/" component={Inicio} />
        <Route exact path="/drogas" component={Drogas} />
        <Route exact path="/drogas/:id" component={DrogasDetalles} />
        <Route exact path="/medicamentos" component={Medicamentos} />
        <Route exact path="/medicamentos/:id" component={MedicamentosDetalles} />
        <Route exact path="/reposiciones" component={Reposiciones} />
        <Route exact path="/reposiciones/:id" component={ReposicionesDetalles} />
        <Route exact path="/partidas" component={Partidas} />
        <Route exact path="/partidas/:id" component={PartidasDetalles} />
        <Route exact path="/stock" component={Stock} />
        <Route exact path="/stock/:id" component={StockDetalles} />
      </Switch>
    </Navbar>
  );
}

export default App;
