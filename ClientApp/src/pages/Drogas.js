import React from "react";
import Grid from "@material-ui/core/Grid";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";
import handleChangePage from "../functions/handleChangePage";
import handleChangeRowsPerPage from "../functions/handleChangeRowsPerPage";

class Drogas extends React.Component {
  state = {
    loading: true,
    error: null,
    data: [],
    pageSize: 5,
    pageNumber: 1,
    totalRecords: 0,
    order: "name",
    search: {
      id: "",
      name: ""
    }
  };
  handleSearch = handleSearch.bind(this);
  handleChangePage = handleChangePage.bind(this);
  handleChangeRowsPerPage = handleChangeRowsPerPage.bind(this);

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.getData());
  }

  async getData(search) {
    this.setState({ error: null });
    try {
      let apiUrl =
        window.ApiUrl +
        "drogas?order=" +
        this.state.order +
        "&pageSize=" +
        this.state.pageSize +
        "&pageNumber=" +
        this.state.pageNumber;
      let response;
      this.state.search.id
        ? (response = await fetch(window.ApiUrl + "drogas/" + this.state.search.id))
        : search
        ? (response = await fetch(apiUrl + search))
        : (response = await fetch(apiUrl));
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
      this.setState({
        data: displayData,
        page: response.headers.get("page"),
        totalRecords: parseInt(response.headers.get("totalRecords"))
      });
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
            <Link to="/drogas/añadir">
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
          </Grid>
        </Grid>

        <MaterialTable
          titles={[["ID", "id"], ["Nombre", "name"]]}
          data={this.state.data}
          currentUrl={"drogas"}
          loading={this.state.loading}
          error={this.state.error}
          handleSearch={this.handleSearch}
          pageSize={this.state.pageSize}
          pageNumber={this.state.pageNumber}
          totalRecords={this.state.totalRecords}
          handleChangePage={this.handleChangePage}
          handleChangeRowsPerPage={this.handleChangeRowsPerPage}
        />
      </React.Fragment>
    );
  }
}

export default Drogas;
