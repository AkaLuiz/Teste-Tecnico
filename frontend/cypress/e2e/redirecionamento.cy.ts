it("deve redirecionar para login sem token", () => {
  localStorage.clear();

  cy.visit("/registros");

  cy.url().should("eq", Cypress.config().baseUrl + "/");
});