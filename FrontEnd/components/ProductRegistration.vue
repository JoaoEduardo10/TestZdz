<template>
  <v-app id="inspire">
    <v-content>
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
    </v-content>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { ApiResponseError } from "~/interfaces/apiResponseError";

export default defineComponent({
  name: "ProductRegistration",
  props: {
    source: String,
  },
  data() {
    return {
      product: {
        name: "",
        value: "",
      },
    };
  },
  methods: {
    async submitForm() {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            name: this.product.name,
            value: Number(this.product.value),
          }),
        });

        if (response.status == 204) {
          alert("Produto ctiado com sucesso!");
          return;
        }

        const data: ApiResponseError<null> = await response.json();

        alert(`Error: ${data.ErrorMessage.Message}`);
      } catch (error: any) {
        console.log(error);
        return;
      }
    },
  },
});
</script>
