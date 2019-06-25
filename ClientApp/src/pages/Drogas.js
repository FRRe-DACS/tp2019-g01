import React from "react";
import Grid from "@material-ui/core/Grid";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";

class Drogas extends React.Component {
  state = {
    loading: true,
    error: null,
    data: [],
    search: {
      id: "",
      name: ""
    }
  };
  handleSearch = handleSearch.bind(this);

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.getData());
  }

  async getData(search) {
    this.setState({ error: null });
    try {
      let response;
      this.state.search.id
        ? (response = await fetch(window.ApiUrl + "drogas/" + this.state.search.id))
        : search
        ? (response = await fetch(window.ApiUrl + "drogas?order=name" + search))
        : (response = await fetch(window.ApiUrl + "drogas?order=name"));
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      let data = await response.json();
      if (!Array.isArray(data)) {
        data = [data];
      }
      const displayData = [];
      data.forEach(function(drug) {
        displayData.push([drug.id, drug.name]);
      });
      this.setState({ data: displayData });
    } catch (error) {
      this.setState({ error: error });
    } finally {
      this.setState({ loading: false });
    }
  }

  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Drogas</h1>
          </Grid>
          <Grid item>
            <Link to="/Drogas/Añadir">
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
          </Grid>
        </Grid>

        <MaterialTable
          titles={[["ID", "id"], ["Nombre", "name"]]}
          data={this.state.data}
          currentUrl={"Drogas"}
          loading={this.state.loading}
          error={this.state.error}
          handleSearch={this.handleSearch}
        />
      </React.Fragment>
    );
  }
}

export default Drogas;
