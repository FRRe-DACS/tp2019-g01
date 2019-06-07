function changeMode() {
  if (this.props.match.params.id === "Añadir") {
    this.setState({ mode: "create" });
  } else {
    const params = new URLSearchParams(this.props.history.location.search);
    if (params.get("mode") === "update") {
      this.setState({ mode: "update" });
    } else if (params.get("mode") === "delete") {
      this.setState({ mode: "delete" });
    }
  }
}

export default changeMode;
