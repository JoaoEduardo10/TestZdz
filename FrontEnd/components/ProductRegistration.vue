<template>
  <v-app id="inspire">
    <v-main>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Adicionar um novo produto</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="form">
                  <v-text-field
                    v-model="product.name"
                    id="name"
                    name="name"
                    label="Nome do Produto"
                    type="text"
                  ></v-text-field>
                  <v-text-field
                    v-model="product.value"
                    id="value"
                    name="value"
                    label="Valor"
                    type="number"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="submitForm">Cadastrar</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "ProductRegistration",
  data() {
    return {
      product: {
        name: "",
        value: 0,
      },
    };
  },
  methods: {
    async submitForm() {
      if (!this.product.name || !this.product.value) {
        alert("Não foi possível registrar o produto");
        return;
      }

      try {
        const response = await fetch(`${this.$config.url_base}/product`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(this.product),
        });

        if (response.status === 204) {
          alert("Produto cadastrado com sucesso!");
          this.product.name = "";
          this.product.value = 0;
          return;
        }

        const errorData = await response.json();
        alert(`Erro: ${errorData.ErrorMessage.Message}`);
      } catch (error) {
        alert(
          "Erro ao cadastrar produto. Por favor, tente novamente mais tarde."
        );
      }
    },
  },
});
</script>
