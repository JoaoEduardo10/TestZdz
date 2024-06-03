<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12>
          <v-card class="elevation-12">
            <v-card-title>Lista de Produtos</v-card-title>
            <v-data-table
              :items="formattedProducts"
              :headers="headers"
              :items-per-page="5"
            >
              <template v-slot:item.edit="{ item }">
                <v-btn class="blue" icon @click="editProduct(item)">
                  <v-icon>mdi-pencil</v-icon>
                </v-btn>
              </template>
              <template v-slot:item.delete="{ item }">
                <v-btn class="red" icon @click="deleteProduct(item.id)">
                  <v-icon>mdi-delete</v-icon>
                </v-btn>
              </template>
            </v-data-table>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed } from "vue";
import { ApiResponse } from "~/interfaces/apiResponse";

export default defineComponent({
  name: "ProductList",
  setup() {
    const products = ref([]);
    const headers = ref([
      { text: "Nome", value: "name" },
      { text: "Valor", value: "valueFormatted" },
      { text: "Editar", value: "edit", sortable: false },
      { text: "Excluir", value: "delete", sortable: false },
    ]);

    const fetchProducts = async () => {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");

        const data: ApiResponse<any> = await response.json();

        if (data.success) {
          products.value = data.result;
        } else {
          alert(`Erro: ${data.message}`);
        }
      } catch (error) {
        alert(
          "Erro ao buscar produtos. Por favor, tente novamente mais tarde."
        );
      }
    };

    const formatCurrency = (value: number) => {
      return value.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    };

    const formattedProducts = computed(() => {
      return products.value.map((product: any) => ({
        ...product,
        valueFormatted: formatCurrency(product.value),
      }));
    });

    const editProduct = (product: any) => {};

    const deleteProduct = async (id: number) => {
      if (
        confirm(
          "Tem certeza que deseja excluir este produto?Também será apagado os pedidos relacionados a ele!"
        )
      ) {
        try {
          const response = await fetch(
            `http://localhost:5042/api/v1/product/${id}`,
            {
              method: "DELETE",
            }
          );

          if (response.ok) {
            alert("Produto excluído com sucesso!");
            fetchProducts(); // Refresh the product list
          } else {
            const data = await response.json();
            alert(`Erro ao excluir produto: ${data.message}`);
          }
        } catch (error) {
          alert(
            "Erro ao excluir produto. Por favor, tente novamente mais tarde."
          );
        }
      }
    };

    onMounted(fetchProducts);

    return {
      headers,
      formattedProducts,
      editProduct,
      deleteProduct,
    };
  },
});
</script>
