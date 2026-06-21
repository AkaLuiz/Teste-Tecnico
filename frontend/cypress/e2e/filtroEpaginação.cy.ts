describe("Filtrar e paginar", () => {
  beforeEach(() => {
    cy.request({
      method: "POST",
      url: "http://localhost:5167/auth/login",
      body: {
        email: "admin@example.com",
        senha: "admin123",
      },
    }).then((response) => {
      localStorage.setItem("token", response.body.token);
      localStorage.setItem(
        "tokenExpiration",
        (Date.now() + 3600000).toString(),
      );
    });
  });

  it("deve avançar página", () => {
    cy.visit("/registros");

    cy.contains("Próxima").click();

    cy.contains("Anterior").click();
  });

  it("deve filtrar contratos", () => {
    cy.visit("/registros");

    cy.contains("Contratos").click();

    cy.contains(/^Contrato$/);

    cy.contains("Procurações").click();

    cy.contains("Procuracao");

    cy.contains("Notificações").click();

    cy.contains("Notificacao");

    cy.contains("Limpar filtros").click();
  });
});
